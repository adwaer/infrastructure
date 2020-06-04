using In.Common;
using In.Cqrs.Query.Queries;
using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.Entity.Abstract;

namespace In.Cqrs.Query.Simple
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly IDiScope _diScope;

        public QueryBuilder(IDiScope diScope)
        {
            _diScope = diScope;
        }

        public IQueryFor<TResult> For<TResult>()
        {
            return _diScope.Resolve<IQueryFor<TResult>>();
        }

        public IGenericQueryBuilder<TSource> ForGeneric<TSource>() where TSource : class, IHasKey
        {
            return _diScope.Resolve<IGenericQueryBuilder<TSource>>();
        }
    }
}