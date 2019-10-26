using System;
using System.Linq.Expressions;

namespace In.Specifications.Helpers.BooleanOperators
{
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        internal NotSpecification(Specification<T> spec)
        {
            _spec = spec ?? throw new ArgumentException(nameof(spec)); 
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _spec.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
        }
    }
}
