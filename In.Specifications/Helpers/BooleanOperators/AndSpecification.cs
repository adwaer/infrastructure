using System;
using System.Linq.Expressions;
using In.Specifications.Helpers.ExpressionCombining;

namespace In.Specifications.Helpers.BooleanOperators
{
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec1;
        private readonly Specification<T> _spec2;

        internal AndSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            _spec1 = spec1 ?? throw new ArgumentException(nameof(spec1));
            _spec2 = spec2 ?? throw new ArgumentException(nameof(spec2));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = _spec1.ToExpression();
            var expr2 = _spec2.ToExpression();
            return expr1.AndAlso(expr2);
        }
    }
}
