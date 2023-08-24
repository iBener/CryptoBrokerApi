using CryptoBroker.EventBus.Commands;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.EventBus;

public static class ApplicationEventBusExtensions
{

    public static IServiceCollection AddApplicationEventBus<T>(this IServiceCollection services, Func<Rebus.Bus.IBus, Task>? onCreated = null)
    {
        services.AddRebus(rebus => rebus
            .Routing(r =>
                r.TypeBased().MapAssemblyOf<OrderCreatedCommand>($"crypto-que"))
            .Transport(t =>
                t.UseRabbitMq(connectionString: "amqp://crypto.rabbitmq:5672", $"crypto-que"))
            //.Sagas(s => s.StoreInMemory())
            , onCreated: onCreated);

        services.AutoRegisterHandlersFromAssemblyOf<T>();

        return services!;
    }
}
