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

namespace CryptoBroker.OrderService.Domain.EventBus;

public class OrderCreatedHandler : IConsumer<OrderCreatedCommand>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly ILogger<OrderCreatedHandler> _logger;

    public OrderCreatedHandler(CryptoDbContext context, IMapper mapper, IBus bus, ILogger<OrderCreatedHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedCommand> context)
    {
        _logger.LogInformation("CONSUME OrderCreatedCommand {Id}", context.Message.Order.Id);

        var message = context.Message;
        var order = _mapper.Map<Order>(message.Order);

        // Save order
        _context.Add(order);
        // Save channels
        await SaveChannels(message.Order);

        await _context.SaveChangesAsync();

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
