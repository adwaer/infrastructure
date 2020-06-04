using In.Common;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Queries;

namespace In.Cqrs.Query.Simple
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
