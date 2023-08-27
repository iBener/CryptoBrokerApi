using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models.Enums;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.EventBus;

public class OrderCancelledNotifyHandler : IConsumer<OrderCancelledNotifyCommand>
{
    private readonly CryptoDbContext _context;

    public OrderCancelledNotifyHandler(CryptoDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<OrderCancelledNotifyCommand> context)
    {
        var message = context.Message;
        var order = await _context.Orders.FindAsync(message.OrderId);
        if (order != null)
        {
            order.Status = (byte)OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
    }
}
