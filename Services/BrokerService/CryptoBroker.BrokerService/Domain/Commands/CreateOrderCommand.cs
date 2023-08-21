using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderCommand : IRequest<OrderModel>
{
    public OrderModel Order { get; }

    public CreateOrderCommand(OrderModel order)
    {
        Order = order;
    }
}
