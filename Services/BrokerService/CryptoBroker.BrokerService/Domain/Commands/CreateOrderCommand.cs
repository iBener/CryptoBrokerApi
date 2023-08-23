using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderCommand : IRequest<OrderModel>
{
    public CreateOrderRequestModel Order { get; }

    public CreateOrderCommand(CreateOrderRequestModel order)
    {
        Order = order;

        if (order.NotificationChannels?.Any() ?? false)
        {
            var types = new List<string>();
            foreach (var nt in order.NotificationChannels)
            {
                if (Enum.TryParse(nt, ignoreCase: true, out NotificationType notificationType) && Enum.IsDefined(notificationType))
                {
                    types.Add(notificationType.ToString());
                }
            }
            order.NotificationChannels = types;
        }
    }
}
