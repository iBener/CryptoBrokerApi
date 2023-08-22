using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Queries;

public class GetOrdersQuery : IRequest<List<OrderModel>>
{
    public GetOrdersQueryModel Query { get; }

    public OrderStatus? Status { get; set; }

    public GetOrdersQuery(GetOrdersQueryModel query)
    {
        Query = query;

        if (Enum.TryParse(query.Status, ignoreCase: true, out OrderStatus status) && Enum.IsDefined(status))
        {
            Status = status;
        }
    }
}
