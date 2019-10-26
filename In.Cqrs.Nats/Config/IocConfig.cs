using In.Cqrs.Nats.Abstract;
using Microsoft.Extensions.DependencyInjection;
using NATS.Client;

namespace In.Cqrs.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddNats(this IServiceCollection services, NatsSenderOptions natsSenderOptions)
        {
            return services.AddScoped<INatsSerializer, NatsSerializer>()
                .AddSingleton<INatsConnectionFactory>(cf =>
                {
                    var serializer = (INatsSerializer) cf.GetService(typeof(INatsSerializer));

                    var natsOptions = ConnectionFactory.GetDefaultOptions();
                    natsOptions.Url = natsSenderOptions.Url;
                    natsOptions.Timeout = 60000;
                    natsOptions.User = natsSenderOptions.User;
                    natsOptions.Password = natsSenderOptions.Password;

                    return new NatsConnectionFactory(serializer, natsOptions);
                });
        }
    }
}