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
                var host = configuration["RabbitMqOptions:Host"];
                var username = configuration["RabbitMqOptions:Username"];
                var passsword = configuration["RabbitMqOptions:Password"];

                defineConsumers?.Invoke(x);

                var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
                if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
                {
                    host = "myrabbitmq.container";
                }

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, c =>
                    {
                        c.Username(username);
                        c.Password(passsword);
                    });

                    defineReceiveEndpoints?.Invoke(context, cfg);
                });
            });
            services.AddSingleton<IMessageBus, MessageBus>();

            return services;
        }
    }
}