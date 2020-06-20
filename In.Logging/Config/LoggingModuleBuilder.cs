using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Logging.Config
{
    public class LoggingModuleBuilder : CoreModuleBuilder
    {
        public LoggingModuleBuilder(IServiceCollection collection) : base(collection)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddLoggerServices();
        }
    }
}