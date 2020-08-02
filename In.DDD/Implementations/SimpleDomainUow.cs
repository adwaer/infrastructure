﻿using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;

namespace In.DDD.Implementations
{
    public class SimpleDomainUow<TAggregate, TModel> : IDomainUow<TAggregate, TModel>
        where TModel : class, IHasKey
        where TAggregate : IAggregateRoot<TModel>
    {
        private readonly IRepository<TModel> _dbRepo;
        private readonly IDomainMessageDispatcher<TAggregate> _dispatcher;
        public IDomainRepository<TAggregate, TModel> Repository { get; }

        public SimpleDomainUow(IDomainRepository<TAggregate, TModel> repository,
            IRepository<TModel> dbRepo,
            IDomainMessageDispatcher<TAggregate> dispatcher)
        {
            _dbRepo = dbRepo;
            _dispatcher = dispatcher;
            Repository = repository;
        }

        public async Task Commit()
        {
            foreach (var domainMessage in Repository.GetDomainMessages())
            {
                await _dispatcher.Dispatch(domainMessage);
            }

            await _dbRepo.Save();
        }
    }
}