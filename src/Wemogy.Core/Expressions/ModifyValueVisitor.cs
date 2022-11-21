using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapster;

namespace Wemogy.Core.Expressions
{
    public class ModifyValueVisitor : ExpressionVisitor
    {
        private readonly Dictionary<PropertyInfo, Func<string, string>> _propertyValueModifiers;

        public ModifyValueVisitor(Dictionary<PropertyInfo, Func<string, string>> propertyValueModifiers)
        {
            _propertyValueModifiers = propertyValueModifiers;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = node.Left;
            var right = node.Right;

            if (ExpressionNeedsModification(left, out PropertyInfo? leftPropertyInfo))
            {
                right = ModifyExpression(right, leftPropertyInfo!);
            }
            else if (ExpressionNeedsModification(right, out PropertyInfo? rightPropertyInfo))
            {
                left = ModifyExpression(left, rightPropertyInfo!);
            }
            else
            {
                return base.VisitBinary(node);
            }

            return Expression.MakeBinary(
                node.NodeType,
                left,
                right);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            try
            {
                var method = node.Method;
                if (method.Name == "Contains" && method.DeclaringType == typeof(List<string>))
                {
                    var stringValues = node.Object;
                    var containsValueArgument = node.Arguments[0];


                    if (stringValues != null && ExpressionNeedsModification(containsValueArgument, out PropertyInfo? propertyInfo))
                    {
                        var stringList = Expression.Lambda(stringValues).Compile().DynamicInvoke() as List<string>;
                        if (stringList == null)
                        {
                            return base.VisitMethodCall(node);
                        }

                        var modifier = _propertyValueModifiers[propertyInfo!];
                        stringList = stringList.Select(modifier).ToList();
                        return Expression.Call(
                            Expression.Constant(stringList),
                            method,
                            node.Arguments);
                    }

                    return base.VisitMethodCall(node);
                }

                return base.VisitMethodCall(node);
            }
            catch
            {
                return base.VisitMethodCall(node);
            }
        }

        private Expression ModifyExpression(Expression expression, PropertyInfo propertyInfo)
        {
            var modifier = _propertyValueModifiers[propertyInfo];
            var function = Expression.Lambda(expression).Compile();
            var value = function.DynamicInvoke();
            var modifiedValue = modifier(value.ToString());
            return Expression.Constant(modifiedValue);
        }

        private bool ExpressionNeedsModification(Expression expression, out PropertyInfo? propertyInfo)
        {
            propertyInfo = null;
            return expression is MemberExpression memberExpression && MemberExpressionNeedsModification(memberExpression, out propertyInfo);
        }

        private bool MemberExpressionNeedsModification(MemberExpression memberExpression, out PropertyInfo? propertyInfo)
        {
            if (memberExpression.Member is PropertyInfo memberInfo)
            {
                propertyInfo = memberInfo;
                return _propertyValueModifiers.ContainsKey(memberInfo);
            }

            propertyInfo = null;

            return false;
        }
    }
}
