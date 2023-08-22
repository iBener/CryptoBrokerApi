using CryptoBroker.Models;
using CryptoBroker.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderCommand : IRequest<OrderModel>
{
    public CreateOrderRequestModel Order { get; }

    public CreateOrderCommand(CreateOrderRequestModel order)
    {
        Order = order;
    }
}
