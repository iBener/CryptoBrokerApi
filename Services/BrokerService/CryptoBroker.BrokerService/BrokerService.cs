using CryptoBroker.Application;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Domain.Queries;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models.Requests;
using MediatR;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Publish create order message to bus
        await _bus.Send(new OrderCreatedEvent(result.Id));

        return result;
    }

    public async Task<OrderModel> CancelOrder(string userId, int orderId)
    {
        var command = new CancelOrderCommand(userId, orderId);
        var result = await _mediator.Send(command);
        return result;
    }
}
