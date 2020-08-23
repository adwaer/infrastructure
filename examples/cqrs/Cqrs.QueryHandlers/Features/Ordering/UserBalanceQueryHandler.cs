using System.Threading.Tasks;
using Cqrs.Domain.Features.Ordering.Criteria;
using Cqrs.Domain.Features.Ordering.Models;
using Cqrs.Domain.Features.Ordering.QueryResult;
using Cqrs.Domain.Features.Ordering.Specifications;
using In.Cqrs.Query.Queries;
using In.DataAccess.Repository.Abstract;

namespace Cqrs.QueryHandlers.Features.Ordering
{
    public class UserBalanceQueryHandler: IQueryHandler<UserBalanceCriterion, UserBalanceQueryResult>
    {
        private readonly IQueryBuilder _queryBuilder;

        public UserBalanceQueryHandler(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public async Task<UserBalanceQueryResult> Ask(UserBalanceCriterion criterion)
        {
            var balance = await _queryBuilder
                .ForGeneric<UserBalance>()
                .Where(UserBalanceSpecifications.WithNonZeroId())
                .FirstOrDefaultAsync();
            
            return new UserBalanceQueryResult(balance?.Balance ?? 0);
        }
    }
}