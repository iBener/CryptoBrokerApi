using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class CreateOrderNotificationChannelHandler : IRequestHandler<CreateOrderNotificationChannelCommand>
{
    private readonly BrokerDbContext _context;

    public CreateOrderNotificationChannelHandler(BrokerDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateOrderNotificationChannelCommand request, CancellationToken cancellationToken)
    {
        var channels = request.Model.NotificationChannels?.Select(x => new NotificationChannel
        {
            OrderId = request.Model.Id,
            Type = (byte)Enum.Parse<NotificationType>(x)
        }).ToList();

        if (channels?.Any() ?? false)
        {
            await _context.AddRangeAsync(channels, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
