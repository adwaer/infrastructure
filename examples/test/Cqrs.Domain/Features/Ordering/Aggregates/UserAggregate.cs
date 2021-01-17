using Cqrs.Domain.Features.Ordering.Models;
using In.DDD.Implementations;
using In.FunctionalCSharp;

namespace Cqrs.Domain.Features.Ordering.Aggregates
{
    public class UserAggregate : SimpleIAggregateRoot<UserBalance>
    {
        public Result<UserAggregate> ChangeBalance(decimal amount)
        {
            Model.Balance += amount;

            return Result.Success(this);
        }
    }
}