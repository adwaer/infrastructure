using System.Reflection;
using In.Common.Config;
using In.Cqrs.Command;
using In.Logging.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Simple.Configs
{
    public static class IocExtensions
    {
        /// <summary>
        /// Adds common services
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            var builder = new CommonModuleBuilder(services);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add logging services
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddLogs(this IServiceCollection services)
        {
            var builder = new LoggingModuleBuilder(services);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add command sender and handlers
        /// </summary>
        /// <param name="handlersAssemblies">commands handlers assemblies</param>
        /// <typeparam name="TMsgResult"></typeparam>
        /// <returns></returns>
        public IServiceCollection AddCqrsSimpleCommands(Assembly[] handlersAssemblies)
            where TMsgResult : class, IMessageResult
        {
            var builder = new SimpleCommandModuleBuilder<TMsgResult>(_collection, handlersAssemblies);
            return builder.AddServices();
        }

        /// <summary>
        /// Add query sender and handlers
        /// </summary>
        /// <param name="handlersAssemblies">query handlers assemblies</param>
        /// <returns></returns>
        public IServiceCollection AddCqrsSimpleQueries(Assembly[] handlersAssemblies)
        {
            var builder = new SimpleQueryModuleBuilder(_collection, handlersAssemblies);
            return builder.AddServices();
        }
    }
}