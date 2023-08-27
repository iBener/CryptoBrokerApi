using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models.Enums;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.EventBus;

public class OrderCancelledNotifyHandler : IConsumer<OrderCancelledNotifyCommand>
{
    private readonly CryptoDbContext _context;
    private readonly ILogger<OrderCancelledNotifyHandler> _logger;

    public OrderCancelledNotifyHandler(CryptoDbContext context, ILogger<OrderCancelledNotifyHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCancelledNotifyCommand> context)
    {
        _logger.LogInformation("CONSUME OrderCancelledNotifyCommand {Id}", context.Message.OrderId);

        var message = context.Message;
        var order = await _context.Orders.FindAsync(message.OrderId);
        if (order != null)
        {
            order.Status = (byte)OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
    }
}
