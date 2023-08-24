using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models.Enums;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.EventBus;

public class OrderCancelledHandler : IHandleMessages<OrderCancelledCommand>
{
    private readonly CryptoDbContext _context;

    public OrderCancelledHandler(CryptoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(OrderCancelledCommand message)
    {
        var order = await _context.Orders.FindAsync(message.OrderId);
        if (order != null)
        {
            order.Status = (byte)OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
    }
}
