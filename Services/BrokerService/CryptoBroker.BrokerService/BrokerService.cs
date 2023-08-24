﻿using CryptoBroker.Application;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models;
using CryptoBroker.Models.Requests;
using MediatR;
using Rebus.Bus;

namespace CryptoBroker.BrokerService;

public class BrokerService : ServiceBase, IBrokerService
{
    private readonly IMediator _mediator;
    private readonly IBus _bus;

    public BrokerService(IMediator mediator, IBus bus)
    {
        _mediator = mediator;
        _bus = bus;
    }

    public async Task<OrderModel> CreateOrder(CreateOrderRequestModel order)
    {
        // Create order
        var command = new CreateOrderCommand(order);
        var result = await _mediator.Send(command);

        // Create channels
        var channelCommand = new CreateOrderNotificationChannelCommand(result);
        await _mediator.Send(channelCommand);

        // Publish create order message to order service via bus
        await _bus.Send(new OrderCreatedCommand(result));

        // Publish create order message to notify service via bus
        await _bus.Send(new OrderCreatedNotifyCommand(result));

        return result;
    }

    public async Task<OrderModel> CancelOrder(string userId, int orderId)
    {
        var command = new CancelOrderCommand(userId, orderId);
        var result = await _mediator.Send(command);

        // Publish cancel order message to order service via bus
        await _bus.Send(new OrderCancelledCommand(orderId));

        // Publish cancel order message to notify service via bus
        await _bus.Send(new OrderCancelledNotifyCommand(orderId));

        return result;
    }
}
