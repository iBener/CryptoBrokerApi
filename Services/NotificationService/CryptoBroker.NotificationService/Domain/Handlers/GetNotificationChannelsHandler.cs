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

public class GetNotificationChannelsHandler : IRequestHandler<GetNotificationChannelsQuery, NotificationChannelModel>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationChannelsHandler(CryptoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NotificationChannelModel> Handle(GetNotificationChannelsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders
            .Include(x => x.NotificationChannels)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.OrderId);
        return _mapper.Map<NotificationChannelModel>(entity);
    }
}
