using In.Cqrs.Command.Nats.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddCommands<TMsgResult>(this IServiceCollection services) where TMsgResult : class, IMessageResult
        {
            return services.AddScoped<IMessageSender, NatsMessageBus>()
                .AddSingleton<INatsReceiverCommandQueueFactory, NatsReceiverCommandQueueFactory>()
                .AddTransient<IMessageResult, TMsgResult>();
        }
    }
}