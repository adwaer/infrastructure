using System;
using System.Linq.Expressions;
using In.Specifications.Helpers.BooleanOperators;

namespace In.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
        
        private Func<T, bool> _compiledExpression;
 
        private Func<T, bool> CompiledExpression => _compiledExpression ?? (_compiledExpression = ToExpression().Compile());

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
        
        #region static
        

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec.ToExpression();
        }
        
        /// <summary>
        /// Override operator true for supporting short-circuit &amp &amp and || operators
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static bool operator true(Specification<T> spec)
        {
            return false;
        }

        /// <summary>
        /// Override operator false for supporting short-circuit &amp &amp and || operators
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static bool operator false(Specification<T> spec)
        {
            return false;
        }

        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator !(Specification<T> spec)
        {
            return new NotSpecification<T>(spec);
        }
        
        #endregion

    }
}
