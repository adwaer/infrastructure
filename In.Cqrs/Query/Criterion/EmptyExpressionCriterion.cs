using System;
using System.Linq.Expressions;
using In.Cqrs.Query.Criterion.Abstract;
using In.Domain;

namespace In.Cqrs.Query.Criterion
{
    public class EmptyExpressionCriterion<T> : IExpressionCriterion<T> where T : class, IEntity<int>
    {
        public Expression<Func<T, bool>> Get()
        {
            return entity => true;
        }
    }
}
