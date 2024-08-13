using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Wemogy.Core.Expressions
{
    public static class ExpressionTreeExtensions
    {
        public static Expression<Func<T, bool>> ModifyPropertyValue<T>(
            this Expression<Func<T, bool>> expression,
            string propertyName,
            Func<string, string> modifier)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var propertyValueModifiers = new Dictionary<PropertyInfo, Func<string, string>>
            {
                { propertyInfo, modifier }
            };

            var visitor = new ModifyValueVisitor(propertyValueModifiers);

            return (Expression<Func<T, bool>>)visitor.Visit(expression)!;
        }
    }
}
