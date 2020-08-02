using In.Specifications;

namespace Cqrs.Domain.Features.Ordering.Specifications
{
    public static class UserBalanceSpecifications
    {
        public static Specification<Models.UserBalance> WithNonZeroId()
        {
            return new DomainSpecification<Models.UserBalance>(ub => ub.Id != 0);
        }
    }
}