using System;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Domain.Features.Ordering.Aggregates;
using In.DDD;

namespace Cqrs.EventHandlers.Features.Ordering
{
    public class WriteConsoleAfterUserChangeEventHandler : IDomainMessageHandler<UserAggregate>
    {
        public Task Handle(UserAggregate args)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"user: {args.Model.UserId}");
            sb.AppendLine($"balance: {args.Model.Balance}");
            sb.AppendLine($"isNewUser: {args.IsNew}");
            Console.WriteLine(sb.ToString());
            
            return Task.CompletedTask;
        }
    }
}