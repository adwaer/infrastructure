using System;
using System.Linq.Expressions;
using In.Specifications;

namespace Identity.Domain
{
    public class DomainSpecification<TAggregateState> : Specification<TAggregateState>
    {
        private readonly Expression<Func<TAggregateState, bool>> _predicate;

        // Constructor should (!!!) have internal modifier to restrict new specifications creation 
        // to only this project. This is because of intent of specification pattern to have all 
        // allowed criterions to query data in one place.
        internal DomainSpecification(Expression<Func<TAggregateState, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentException(nameof(predicate));
        }

        public override Expression<Func<TAggregateState, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
