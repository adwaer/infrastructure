using In.Cqrs.Command.Nats.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddNatsCommandMaster<TMsgResult>(this IServiceCollection services) where TMsgResult : class, IMessageResult
        {
            return services.AddTransient<IMessageSender, NatsMessageBus>()
                .AddTransient<IMessageResult, TMsgResult>();
        }
        
        public static IServiceCollection AddNatsCommandSlave(this IServiceCollection services)
        {
            return services
                .AddSingleton<INatsReceiverCommandQueueFactory, NatsReceiverCommandQueueFactory>()
                .AddTransient<INatsCommandReplyFactory, NatsCommandReplyFactory>();
        }
    }
}