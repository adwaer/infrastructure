using System.Reflection;
using In.Common;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Simple.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddCommandServices<TMsgResult>(this IServiceCollection services,
            Assembly[] assemblies) where TMsgResult : class, IMessageResult
        {
            return services.AddScoped<IMessageSender, SimpleMsgBus>()
                .AddTransient<IMessageResult, TMsgResult>()
                .RegisterAssemblyImplementationsScoped(assemblies, typeof(ICommandHandler<>))
                .RegisterAssemblyImplementationsScoped(assemblies, typeof(ICommandHandler<,>));
        }
    }
}