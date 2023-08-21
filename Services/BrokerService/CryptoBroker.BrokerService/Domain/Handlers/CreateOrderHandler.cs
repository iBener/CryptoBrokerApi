using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderModel>
{
    private readonly BrokerDbContext _context;

    public CreateOrderHandler(BrokerDbContext context)
    {
        _context = context;
    }

    public async Task<OrderModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = request.Order.Id,
            Amount = request.Order.Amount,
            NotificationType = request.Order.NotificationType,
            Price = request.Order.Price,
            UserId = request.Order.UserId
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        var result = new OrderModel
        {
            Id = order.Id,
            Amount = order.Amount,
            NotificationType = order.NotificationType,
            Price = order.Price,
            UserId = order.UserId
        };

        return result;
    }
}
