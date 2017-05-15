using System;
using System.Linq.Expressions;
using In.Cqrs.Query.Criterion.Abstract;
using In.Domain;

namespace In.Cqrs.Query.Criterion
{
    public class ByIdExpressionCriterion<T, TId> : IExpressionCriterion<T> where T : class, IEntity<TId>
    {
        private readonly TId _id;

        public ByIdExpressionCriterion(TId id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> Get()
        {
            return entity => (object) entity.Id == (object) _id;
        }
    }
}