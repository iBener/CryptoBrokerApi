using AutoMapper;
using Azure.Core;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace CryptoBroker.NotificationService.Domain.EventBus;

public class OrderCreatedNotifyHandler : IConsumer<OrderCreatedNotifyCommand>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly ILogger<OrderCreatedNotifyHandler> _logger;

    public OrderCreatedNotifyHandler(CryptoDbContext context, IMapper mapper, IBus bus, ILogger<OrderCreatedNotifyHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedNotifyCommand> context)
    {
        _logger.LogInformation("CONSUME OrderCreatedNotifyCommand {Id}", context.Message.Order.Id);

        var message = context.Message;
        // Send notifications
        if (message.Order.NotificationChannels?.Any() ?? false)
        {
            var order = _mapper.Map<Order>(message.Order);

            foreach (var notification in order.Notifications ?? Enumerable.Empty<Notification>())
            {
                // Sending the notification
                await Task.Delay(3000);

                notification.Success = true;
                notification.Date = DateTime.Now;

                // Send notification success message
                var notificationModel = _mapper.Map<NotificationModel>(notification);
                await _bus.Publish(new NotificationSentCommand(notificationModel));
                _logger.LogInformation("PUBLISH NotificationSentCommand {Id}", order.Id);
            }

            // Save order
            _context.Add(order);
            // Save channels
            await SaveChannels(message.Order);

            await _context.SaveChangesAsync();
        }

        async Task SaveChannels(OrderModel order)
        {
            var channels = order.NotificationChannels?.Select(x => new NotificationChannel
            {
                OrderId = order.Id,
                Type = (byte)Enum.Parse<NotificationType>(x)
            }).ToList();

            if (channels?.Any() ?? false)
            {
                await _context.AddRangeAsync(channels);
            }
        }
    }
}
