using System;
using System.Reflection;
using In.Auth.Config;
using In.Auth.Identity.Server.Config;
using In.Common.Config;
using In.Cqrs.Command;
using In.Cqrs.Command.Nats.Config;
using In.Cqrs.Command.Simple.Config;
using In.Cqrs.Nats.Config;
using In.Cqrs.Query.Nats.Config;
using In.Cqrs.Query.Simple.Config;
using In.DataAccess.EfCore.Config;
using In.DataAccess.Mongo.Config;
using In.DataMapping.Automapper.Config;
using In.DDD.Config;
using In.Logging.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.Web.Nats
{
    public class ProjectBuilder
    {
        private readonly IServiceCollection _collection;

        public ProjectBuilder(IServiceCollection collection)
        {
            _collection = collection;
        }
        
        /// <summary>
        /// Adds common services
        /// </summary
        /// <returns></returns>
        public IServiceCollection AddCommonServices()
        {
            var builder = new CommonModuleBuilder(_collection);
            return builder.AddServices();
            
        }

        /// <summary>
        /// Add logging services
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddLoggingServices()
        {
            var builder = new LoggingModuleBuilder(_collection);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add ddd services
        /// </summary>
        /// <param name="assemblies">assemblies with ddd repositories and event handlers</param>
        /// <returns></returns>
        public IServiceCollection AddDDD(Assembly[] assemblies)
        {
            var builder = new DddModuleBuilder(_collection, assemblies);
            return builder.AddServices();
        }

        /// <summary>
        /// Add identity server connection settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IServiceCollection AddAuth(AuthenticationSettings settings)
        {
            var builder = new AuthModuleBuilder(_collection, settings);
            return builder.AddServices();
        }

        /// <summary>
        /// Add identity server settings
        /// </summary>
        /// <param name="optionsBuilder">db context builder</param>
        /// <typeparam name="TUser">Type of identity user</typeparam>
        /// <typeparam name="TCtx">Ef db context type</typeparam>
        /// <returns></returns>
        public IServiceCollection AddIdentityServer<TUser, TCtx>(Action<DbContextOptionsBuilder> optionsBuilder)
            where TUser : IdentityUser where TCtx : DbContext
        {
            var builder = new IdentityServerModuleBuilder<TUser, TCtx>(_collection, optionsBuilder);
            return builder.AddServices();
        }

        /// <summary>
        /// Add command sender and handlers
        /// </summary>
        /// <param name="handlersAssemblies">commands handlers assemblies</param>
        /// <typeparam name="TMsgResult"></typeparam>
        /// <returns></returns>
        public IServiceCollection AddCqrsSimpleCommands<TMsgResult>(Assembly[] handlersAssemblies)
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

        /// <summary>
        /// Add NATS command sender
        /// </summary>
        /// <param name="natsSettings"></param>
        /// <typeparam name="TMsgResult"></typeparam>
        /// <returns></returns>
        public IServiceCollection AddCqrsNatsCommandSender<TMsgResult>(NatsSenderOptions natsSettings)
            where TMsgResult : class, IMessageResult
        {
            var builder = new NatsCommandMasterModuleBuilder<TMsgResult>(_collection, natsSettings);
            return builder.AddServices();
        }

        /// <summary>
        /// Add NATS command handlers
        /// </summary>
        /// <param name="natsSettings"></param>
        /// <returns></returns>
        public IServiceCollection AddCqrsNatsCommandHandlers(NatsSenderOptions natsSettings)
        {
            var builder = new NatsCommandSlaveModuleBuilder(_collection, natsSettings);
            return builder.AddServices();
        }

        /// <summary>
        /// Add NATS query builder
        /// </summary>
        /// <param name="natsSettings"></param>
        /// <returns></returns>
        public IServiceCollection AddCqrsNatsQueryBuilder(NatsSenderOptions natsSettings)
        {
            var builder = new NatsQueryMasterModuleBuilder(_collection, natsSettings);
            return builder.AddServices();
        }

        /// <summary>
        /// Add NATS query handlers
        /// </summary>
        /// <param name="natsSettings"></param>
        /// <returns></returns>
        public IServiceCollection AddCqrsNatsQueryHandler(NatsSenderOptions natsSettings)
        {
            var builder = new NatsQuerySlaveModuleBuilder(_collection, natsSettings);
            return builder.AddServices();
        }

        /// <summary>
        /// Add ef core providers
        /// </summary>
        /// <param name="repositoryAssemblies"></param>
        /// <typeparam name="TCtx">Ef db context type</typeparam>
        /// <returns></returns>
        public IServiceCollection AddDAEfCoreProviders<TCtx>(Assembly[] repositoryAssemblies) where TCtx : DbContext
        {
            var builder = new DataAccessEfCoreModuleBuilder<TCtx>(_collection, repositoryAssemblies);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add providers for mongo da
        /// </summary>
        /// <param name="repositoryAssemblies"></param>
        /// <returns></returns>
        public IServiceCollection AddDAMongoProviders(Assembly[] repositoryAssemblies)
        {
            var builder = new DataAccessMongoModuleBuilder(_collection, repositoryAssemblies);
            return builder.AddServices();
        }

        
        /// <summary>
        /// Add automapper mappings
        /// </summary>
        /// <param name="assembliesWithProfile"></param>
        /// <returns></returns>
        public IServiceCollection AddDMAutomapper(Assembly[] assembliesWithProfile)
        {
            var builder = new DataMappingAutomapperModuleBuilder(_collection, assembliesWithProfile);
            return builder.AddServices();
        }
    }
}