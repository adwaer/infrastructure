using System;
using System.Linq.Expressions;
using In.Common.Exceptions;

namespace In.Specifications.Helpers.BooleanOperators
{
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        internal NotSpecification(Specification<T> spec)
        {
            _spec = spec ?? throw new  BadRequestException(nameof(spec), "Incorrect specification"); 
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _spec.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
        }
    }
}
