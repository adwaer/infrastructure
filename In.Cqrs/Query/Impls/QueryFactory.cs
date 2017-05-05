using In.Domain;

namespace In.Cqrs.Query.Impls
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IDiScope _diScope;

        public QueryFactory(IDiScope diScope)
        {
            _diScope = diScope;
        }

        public IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return _diScope.Resolve<IQuery<TCriterion, TResult>>();
        }
    }
}
