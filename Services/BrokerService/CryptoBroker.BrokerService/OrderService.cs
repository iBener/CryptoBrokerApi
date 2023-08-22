using CryptoBroker.Application;
using CryptoBroker.BrokerService.Domain.Queries;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CryptoBroker.BrokerService;

public class OrderService : ServiceBase, IOrderService
{
    private readonly IMediator _mediator;

    public OrderService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OrderModel> GetActiveOrder(string userId)
    {
        var query = new GetActiveOrderQuery();
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<OrderModel> GetOrder(string userId, int orderId)
    {
        var query = new GetOrderQuery() { UserId = userId, OrderId = orderId };
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<List<OrderModel>> GetOrders(GetOrdersQueryModel queryModel)
    {
        var query = new GetOrdersQuery(queryModel);
        var result = await _mediator.Send(query);
        return result;
    }
}
