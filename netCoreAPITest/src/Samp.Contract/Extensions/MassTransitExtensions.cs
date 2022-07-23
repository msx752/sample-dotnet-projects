using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Samp.Contract.Extensions
{
    public static class MasstransitExtensions
    {
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services
            , IConfiguration configuration
            , Action<IBusRegistrationConfigurator> defineConsumers = null
            , Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> defineReceiveEndpoints = null

            )
        {
            services.AddMassTransit(x =>
            {
                defineConsumers?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMqOptions:Host"], c =>
                    {
                        c.Username(configuration["RabbitMqOptions:Username"]);
                        c.Password(configuration["RabbitMqOptions:Password"]);
                    });

                    defineReceiveEndpoints?.Invoke(context, cfg);
                });
            });
            services.AddSingleton<IMessageBus, MessageBus>();

            return services;
        }
    }
}