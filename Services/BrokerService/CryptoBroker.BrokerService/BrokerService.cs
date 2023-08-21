using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService;

public class BrokerService : IBrokerService
{
    private readonly IMediator _mediator;

    public BrokerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OrderModel> CreateOrder(OrderModel order)
    {
        var command = new CreateOrderCommand(order);
        var result = await _mediator.Send(command);
        return result;
    }
}
