using System;
using System.Linq.Expressions;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Criterion
{
    public class CustomExpressionCriterion<T> : IExpressionCriterion<T> where T : class
    {
        private readonly Expression<Func<T, bool>> _expression;

        public CustomExpressionCriterion(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public Expression<Func<T, bool>> Get() => _expression;
    }
}
