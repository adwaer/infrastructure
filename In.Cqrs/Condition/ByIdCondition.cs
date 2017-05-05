using System;
using System.Linq.Expressions;
using In.Cqrs.Condition.Abstract;
using In.Domain;

namespace In.Cqrs.Condition
{
    public class ByIdCondition<T, TId> : IExpressionCriterion<T> where T : IEntity<TId>
    {
        private readonly TId _id;

        public ByIdCondition(TId id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> Get()
        {
            return entity => (object) entity.Id == (object) _id;
        }
    }
}