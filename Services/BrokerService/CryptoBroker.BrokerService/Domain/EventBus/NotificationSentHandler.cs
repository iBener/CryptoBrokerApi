using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models.Enums;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace CryptoBroker.BrokerService.Domain.EventBus;

public class NotificationSentHandler : IConsumer<NotificationSentCommand>
{
    private readonly CryptoDbContext _context;
    private readonly ILogger<NotificationSentHandler> _logger;

    public NotificationSentHandler(CryptoDbContext context, ILogger<NotificationSentHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationSentCommand> context)
    {
        _logger.LogInformation("CONSUME NotificationSentCommand {Id}", context.Message.Notification.OrderId);

        var message = context.Message;
        var type = Enum.Parse<NotificationType>(message.Notification.NotificationType);
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(x => x.OrderId == message.Notification.OrderId &&
                                      x.Type == (byte)type);
        if (notification is not null)
        {
            notification.Success = message.Notification.Success;
            notification.Date = message.Notification.Date;
            _context.Update(notification);
        }
        else
        {
            notification = new Entities.Notification
            {
                Date = message.Notification.Date,
                OrderId = message.Notification.OrderId,
                Success = message.Notification.Success,
                Type = (byte)type  
            };
            _context.Add(notification);
        }
        await _context.SaveChangesAsync();
    }
}
