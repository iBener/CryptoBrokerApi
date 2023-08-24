using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.Queries;

public class GetNotificationChannelsQuery : IRequest<NotificationChannelModel>
{
    public GetNotificationChannelsQuery(string userId, int orderId)
    {
        UserId = userId;
        OrderId = orderId;
    }

    public string UserId { get; }
    public int OrderId { get; }
}
