using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.Queries;

public class GetNotificationsQuery : IRequest<List<NotificationModel>>
{
    public GetNotificationsQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}
