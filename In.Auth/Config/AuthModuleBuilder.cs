using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Auth.Config
{
    public class AuthModuleBuilder : CoreModuleBuilder
    {
        private readonly AuthenticationSettings _settings;

        public AuthModuleBuilder(IServiceCollection services, AuthenticationSettings settings)
            : base(services)
        {
            _settings = settings;
        }


        public override IServiceCollection AddServices()
        {
            return Collection
                .AddAuth(_settings);
        }
    }
}