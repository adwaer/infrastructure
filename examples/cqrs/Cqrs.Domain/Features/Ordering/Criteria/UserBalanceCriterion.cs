using In.Cqrs.Query.Criterion.Abstract;

namespace Cqrs.Domain.Features.Ordering.Criteria
{
    public class UserBalanceCriterion : ICriterion
    {
        public int UserId { get; }

        public UserBalanceCriterion(int userId)
        {
            UserId = userId;
        }
    }
}