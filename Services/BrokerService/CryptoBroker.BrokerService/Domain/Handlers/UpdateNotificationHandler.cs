using CryptoBroker.BrokerService.Domain.Commands;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class UpdateNotificationHandler : IHandleMessages<UpdateNotification>
{
    private readonly IBus _bus;

    public UpdateNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(UpdateNotification message)
    {
        // Update order notification status
        await Task.Delay(1000);

        await _bus.Send(new UpdateNotificationCompleted(message.OrderId));
    }
}
