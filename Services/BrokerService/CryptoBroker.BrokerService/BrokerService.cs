using CryptoBroker.Application;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Domain.Queries;
using CryptoBroker.Models;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService;

public class BrokerService : ServiceBase, IBrokerService
{
    private readonly IMediator _mediator;

    public BrokerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OrderModel> CreateOrder(CreateOrderRequestModel order)
    {
        var command = new CreateOrderCommand(order);
        var result = await _mediator.Send(command);
        return result;
    }

    public Task<OrderModel> CancelOrder(int id, string userId)
    {
        throw new NotImplementedException();
    }
}
