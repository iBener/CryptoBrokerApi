using AutoMapper;
using CryptoBroker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoBroker.NotificationService.Domain.Queries;
using MediatR;

namespace CryptoBroker.NotificationService;

public class NotificationService : INotificationService
{
    private readonly IMediator _mediator;

    public NotificationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<NotificationModel>> GetNotifications(string userId)
    {
        var query = new GetNotificationsQuery(userId);
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<NotificationChannelModel> GetNotificationTypes(string userId, int orderId)
    {
        var query = new GetNotificationChannelsQuery(userId, orderId);
        var result = await _mediator.Send(query);
        return result;
    }
}
