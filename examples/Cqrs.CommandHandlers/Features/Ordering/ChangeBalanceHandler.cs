using System.Threading.Tasks;
using Cqrs.Domain.Features.Ordering.Aggregates;
using Cqrs.Domain.Features.Ordering.Commands;
using Cqrs.Domain.Features.Ordering.Models;
using Cqrs.Domain.Features.Ordering.Specifications;
using In.Cqrs.Command;
using In.DDD;
using In.FunctionalCSharp;

namespace Cqrs.CommandHandlers.Features.Ordering
{
    public class ChangeBalanceHandler : ICommandHandler<ChangeBalanceCmd>
    {
        private readonly IDomainUow<UserAggregate, UserBalance> _domainUow;
        private readonly IDomainRepository<UserAggregate, UserBalance> _repository;

        public ChangeBalanceHandler(IDomainUow<UserAggregate, UserBalance> domainUow)
        {
            _domainUow = domainUow;
            _repository = domainUow.Repository;
        }

        public async Task<Result> Handle(ChangeBalanceCmd message)
        {
            var aggregate = await _repository
                .FindOne(UserBalanceSpecifications.WithNonZeroId());

            aggregate ??= _repository.Create();

            return await aggregate.ChangeBalance(message.Amount)
                .Tap(async aggr =>
                {
                    if (!aggregate.IsNew)
                        _repository.Update(aggr);
                    
                    await _domainUow.Commit();
                });
        }
    }
}