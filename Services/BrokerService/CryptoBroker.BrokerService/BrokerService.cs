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

public class BrokerService : IBrokerService
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

    public async Task<OrderModel> GetOrder(int id)
    {
        var query = new GetOrderQuery() { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<List<OrderModel>> GetOrders(GetOrdersQueryModel queryModel)
    {
        var query = new GetOrdersQuery(queryModel);
        var result = await _mediator.Send(query);
        return result;
    }

    public Task<OrderModel> CancelOrder(int id)
    {
        throw new NotImplementedException();
    }
}
