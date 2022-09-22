using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mapster;

namespace Wemogy.Core.Expressions
{
    internal class ConversionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _newParameter;
        private readonly ParameterExpression _oldParameter;
        private readonly string _prefix;

        public ConversionVisitor(ParameterExpression newParameter, ParameterExpression oldParameter, string prefix)
        {
            _newParameter = newParameter;
            _oldParameter = oldParameter;
            _prefix = prefix;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _newParameter; // replace all old param references with new ones
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = node.Left;
            var right = node.Right;

            if (left is not MemberExpression leftModeMemberExpression)
            {
                return base.VisitBinary(node);
            }

            var propertyName = leftModeMemberExpression.Member.Name;
            if (propertyName == "Id")
            {
                var function = Expression.Lambda(node.Right).Compile();
                var value = function.DynamicInvoke();

                var valueString = $"{_prefix}{value}";
                right = Expression.Constant(valueString);
            }

            left = Expression.Property(
                _newParameter,
                propertyName);

            return Expression.MakeBinary(
                node.NodeType,
                left,
                right);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            try
            {
                if (node.Object == null)
                {
                    throw new NotImplementedException("This case is not supported");
                }

                var containsMethod = typeof(List<string>).GetMethod(nameof(List<string>.Contains), new[] { typeof(string) });
                if (containsMethod == null)
                {
                    throw new Exception("A list of string should always have a Contains method");
                }

                var listDelegate = Expression.Lambda(node.Object).Compile();
                var list = listDelegate.DynamicInvoke();
                var stringList = list
                    .Adapt<List<string>>()
                    .Select(x => $"{_prefix}{x}")
                    .ToList();

                var args = node.Arguments
                    .Select(arg => arg is MemberExpression memberArg ? VisitMember(memberArg) : arg)
                    .ToList();

                return Expression.Call(
                    Expression.Constant(stringList),
                    containsMethod,
                    args);
            }
            catch
            {
                return base.VisitMethodCall(node);
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            // if instance is not old parameter - do nothing
            if (node.Expression != _oldParameter)
            {
                return base.VisitMember(node);
            }

            var propertyName = node.Member.Name;

            return Expression.Property(
                _newParameter,
                propertyName);
        }
    }
}
