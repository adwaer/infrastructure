using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common.Config
{
    public class CommonModuleBuilder : CoreModuleBuilder
    {
        public CommonModuleBuilder(IServiceCollection collection) : base(collection)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddCommonServices();
        }
    }
}