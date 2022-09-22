using System;
using System.Linq;
using System.Linq.Expressions;

namespace Wemogy.Core.Expressions
{
    public static class ExpressionTreeConverter
    {
        public static Expression<Func<TNewParameter, bool>> ReplaceFunctionalBinaryExpressionParameterType<TOldParameter, TNewParameter>(
            this Expression<Func<TOldParameter, bool>> expression, string prefix = "")
        {
            var oldParameter = expression.Parameters.First();
            var newParameter = Expression.Parameter(typeof(TNewParameter), oldParameter.Name);
            var converter = new ConversionVisitor(newParameter, oldParameter, prefix);
            var newBody = converter.Visit(expression.Body);

            return Expression.Lambda<Func<TNewParameter, bool>>(newBody, newParameter);
        }
    }
}
