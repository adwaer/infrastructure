using In.Cqrs.Command.Nats.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddNatsCommandServices<TMsgResult>(this IServiceCollection services) where TMsgResult : class, IMessageResult
        {
            return services.AddScoped<IMessageSender, NatsMessageBus>()
                .AddTransient<IMessageResult, TMsgResult>();
        }
        
        public static IServiceCollection AddNatsCommandFactoryServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<INatsReceiverCommandQueueFactory, NatsReceiverCommandQueueFactory>()
                .AddSingleton<INatsCommandReplyFactory, NatsCommandReplyFactory>();
        }
    }
}