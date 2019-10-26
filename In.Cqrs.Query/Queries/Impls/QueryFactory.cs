using In.Common;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Queries.Impls
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IDiScope _diScope;

        public QueryFactory(IDiScope diScope)
        {
            _diScope = diScope;
        }

        public IQueryHandler<TCriterion, TResult> Get<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return _diScope.Resolve<IQueryHandler<TCriterion, TResult>>();
        }
    }
}
