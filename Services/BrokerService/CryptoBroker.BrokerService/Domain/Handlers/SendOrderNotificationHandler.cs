using CryptoBroker.BrokerService.Domain.Commands;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class SendOrderNotificationHandler : IHandleMessages<SendOrderNotification>
{
    private readonly IBus _bus;

    public SendOrderNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(SendOrderNotification message)
    {
        // Sending the notifications
        await Task.Delay(3000);

        // Push OrderNotificationSent event
        await _bus.Send(new OrderNotificationSent(message.OrderId));
    }
}
