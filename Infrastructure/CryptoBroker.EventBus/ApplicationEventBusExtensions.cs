using CryptoBroker.EventBus.Commands;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
//using Rebus.Activation;
//using Rebus.Config;
//using Rebus.Retry.Simple;
//using Rebus.Routing.TypeBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.EventBus;

public static class ApplicationEventBusExtensions
{
    public static IServiceCollection AddApplicationEventBus<T>(this IServiceCollection services)
        //, Func<Rebus.Bus.IBus, Task>? onCreated = null)
    {
        //services.AddRebus(rebus => rebus
        //    .Options(b => b.SimpleRetryStrategy(maxDeliveryAttempts: 10))
        //    .Routing(r =>
        //        r.TypeBased().MapAssemblyOf<OrderCreatedCommand>($"crypto-que"))
        //    .Transport(t =>
        //        t.UseRabbitMq(connectionString: "amqp://crypto.rabbitmq:5672", $"crypto-que"))
        //    //.Sagas(s => s.StoreInMemory())
        //    , onCreated: onCreated);
        //services.AutoRegisterHandlersFromAssemblyOf<T>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            var assembly = typeof(T).Assembly;
            x.AddConsumers(assembly);
            x.AddSagaStateMachines(assembly);
            x.AddSagas(assembly);
            x.AddActivities(assembly);

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri("amqp://crypto.rabbitmq:5672"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });

        });

        return services!;
    }
}
