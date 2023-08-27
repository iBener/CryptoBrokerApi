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

public class OrderCancelledHandler : IConsumer<OrderCancelledCommand>
{
    private readonly CryptoDbContext _context;
    private readonly ILogger<OrderCancelledHandler> _logger;

    public OrderCancelledHandler(CryptoDbContext context, ILogger<OrderCancelledHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCancelledCommand> context)
    {
        _logger.LogInformation("CONSUME OrderCancelledCommand {Id}", context.Message.OrderId);

        var message = context.Message;
        var order = await _context.Orders.FindAsync(message.OrderId);
        if (order != null)
        {
            order.Status = (byte)OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
    }
}
