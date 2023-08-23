using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Queries;

public class GetActiveOrderQuery : IRequest<OrderModel>
{
    public string UserId { get; set; } = String.Empty;
}
