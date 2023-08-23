using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderNotificationChannelCommand : IRequest
{
    public OrderModel Model { get; }

    public CreateOrderNotificationChannelCommand(OrderModel result)
    {
        Model = result;
    }
}
