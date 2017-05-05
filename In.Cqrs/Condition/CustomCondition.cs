using System;
using System.Linq.Expressions;
using In.Cqrs.Condition.Abstract;

namespace In.Cqrs.Condition
{
    public class CustomCondition<T> : IExpressionCriterion<T> where T : class
    {
        private readonly Expression<Func<T, bool>> _expression;

        public CustomCondition(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public Expression<Func<T, bool>> Get() => _expression;
    }
}
