using Identity.Domain.Models;
using In.Specifications;

namespace Identity.Domain.Features.Profile.Specifications
{
    public static class UserSpecifications
    {
        public static Specification<User> WithId(string id)
        {
            return new DomainSpecification<User>(u => u.Id == id);
        }
    }
}