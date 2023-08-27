using CryptoBroker.Application;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.Models;
using CryptoBroker.Models.Requests;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoBroker.BrokerService;

public class BrokerService : ServiceBase, IBrokerService
{
    private readonly IMediator _mediator;
    private readonly IBus _bus;
    private readonly ILogger<BrokerService> _logger;

    public BrokerService(IMediator mediator, IBus bus, ILogger<BrokerService> logger)
    {
        _mediator = mediator;
        _bus = bus;
        _logger = logger;
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
        await _bus.Publish(new OrderCreatedCommand(result));
        _logger.LogInformation("PUBLISH OrderCreatedCommand {Id}", result.Id);

        // Publish create order message to notify service via bus
        await _bus.Publish(new OrderCreatedNotifyCommand(result));
        _logger.LogInformation("PUBLISH OrderCreatedNotifyCommand {Id}", result.Id);

        return result;
    }

    public async Task<OrderModel> CancelOrder(string userId, int orderId)
    {
        var command = new CancelOrderCommand(userId, orderId);
        var result = await _mediator.Send(command);

        // Publish cancel order message to order service via bus
        await _bus.Publish(new OrderCancelledCommand(orderId));
        _logger.LogInformation("PUBLISH OrderCancelledCommand {Id}", result.Id);

        // Publish cancel order message to notify service via bus
        await _bus.Publish(new OrderCancelledNotifyCommand(orderId));
        _logger.LogInformation("PUBLISH OrderCancelledNotifyCommand {Id}", result.Id);

        return result;
    }
}
