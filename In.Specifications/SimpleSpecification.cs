using System;
using System.Linq.Expressions;
using In.Common.Exceptions;

namespace In.Specifications
{
    public class SimpleSpecification<TAggregateState> : Specification<TAggregateState>
    {
        private readonly Expression<Func<TAggregateState, bool>> _predicate;

        // Constructor should (!!!) have internal modifier to restrict new specifications creation 
        // to only this project. This is because of intent of specification pattern to have all 
        // allowed criterions to query data in one place.
        internal SimpleSpecification(Expression<Func<TAggregateState, bool>> spec)
        {
            _predicate = spec ?? throw new  BadRequestException(nameof(spec), "Incorrect specification");
        }

        public override Expression<Func<TAggregateState, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
