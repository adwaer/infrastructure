using Microsoft.Extensions.DependencyInjection;

namespace In.AppBuilder
{
    public abstract class CoreModuleBuilder
    {
        protected IServiceCollection Collection { get; }

        protected CoreModuleBuilder(IServiceCollection collection)
        {
            Collection = collection;
        }

        public abstract IServiceCollection AddServices();
    }
}