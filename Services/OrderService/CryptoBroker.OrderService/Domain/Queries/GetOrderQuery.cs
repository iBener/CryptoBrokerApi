using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService.Domain.Queries;

public class GetOrderQuery : IRequest<OrderModel>
{
    public int OrderId { get; set; }

    public string UserId { get; set; } = string.Empty;
}
