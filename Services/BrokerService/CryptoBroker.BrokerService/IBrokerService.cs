using CryptoBroker.Application;
using CryptoBroker.Models;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService;

public interface IBrokerService : IServiceBase
{
    Task<OrderModel> CreateOrder(CreateOrderRequestModel order);
    Task<OrderModel> CancelOrder(string userId, int orderId);
}
