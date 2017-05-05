using In.Domain;

namespace In.Cqrs.Query.Impls
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
    }
}