using System;
using System.Linq.Expressions;
using In.Cqrs.Query;

namespace In.Cqrs.Condition.Abstract
{
    public interface IExpressionCriterion<T> : IGenericCriterion<T>
    {
        Expression<Func<T, bool>> Get();
    }
}
