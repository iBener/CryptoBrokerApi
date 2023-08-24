using AutoMapper;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models;
using CryptoBroker.NotificationService.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Domain.Handlers;

public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, List<NotificationModel>>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationsHandler(CryptoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<NotificationModel>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Notifications
            .Where(x => x.Order.UserId == request.UserId)
            .OrderBy(x => x.Date)
            .ToListAsync();
        return _mapper.Map<List<NotificationModel>>(entities);
    }
}
