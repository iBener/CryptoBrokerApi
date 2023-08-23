using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderSaga : Saga<CreateOrderSagaData>, 
    IAmInitiatedBy<OrderCreatedEvent>,
    IHandleMessages<OrderNotificationSent>,
    IHandleMessages<UpdateNotificationCompleted>
{
    private readonly IBus _bus;

    public CreateOrderSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<CreateOrderSagaData> config)
    {
        config.Correlate<OrderCreatedEvent>(message => message.OrderId, s => s.OrderId);
        config.Correlate<OrderNotificationSent>(message => message.OrderId, s => s.OrderId);
        config.Correlate<UpdateNotificationCompleted>(message => message.OrderId, s => s.OrderId);
    }

    // Saga 1. adım
    public async Task Handle(OrderCreatedEvent message)
    {
        if (IsNew)
        {
            await _bus.Send(new SendOrderNotification(message.OrderId));
        }
    }

    // Saga 2. adım
    public async Task Handle(OrderNotificationSent message)
    {
        Data.NotificationsSent = true;

        await _bus.Send(new UpdateNotification(message.OrderId));
    }

    // Saga 3. adım. Completed
    public Task Handle(UpdateNotificationCompleted message)
    {
        Data.OrderUpdated = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}

public class CreateOrderSagaData : ISagaData
{
    public Guid Id { get; set; }
    
    public int Revision { get; set; }

    public int OrderId { get; set; }

    public bool NotificationsSent { get; set; }

    public bool OrderUpdated { get; set; }
}

public record OrderCreatedEvent(int OrderId);

public record SendOrderNotification(int OrderId);

public record OrderNotificationSent(int OrderId);

public record UpdateNotification(int OrderId);

public record UpdateNotificationCompleted(int OrderId);