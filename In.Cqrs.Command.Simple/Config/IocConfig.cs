using System.Reflection;
using In.Common;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Simple.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddCommandServices<TMsgResult>(this IServiceCollection services,
            params Assembly[] assemblies) where TMsgResult : class, IMessageResult
        {
            return services.AddTransient<IMessageSender, SimpleMsgBus>()
                .AddTransient<IMessageResult, TMsgResult>()
                .RegisterAssemblyImplementationsTransient(typeof(ICommandHandler<>), assemblies)
                .RegisterAssemblyImplementationsTransient(typeof(ICommandHandler<,>), assemblies);
        }
    }
}