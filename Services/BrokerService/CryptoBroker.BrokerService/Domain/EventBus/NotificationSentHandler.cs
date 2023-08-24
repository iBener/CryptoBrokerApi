using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.EventBus;

public class NotificationSentHandler : IHandleMessages<NotificationSentCommand>
{
    private readonly CryptoDbContext _context;

    public NotificationSentHandler(CryptoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(NotificationSentCommand message)
    {
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
