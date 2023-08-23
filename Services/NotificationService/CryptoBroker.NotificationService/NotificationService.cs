using AutoMapper;
using CryptoBroker.NotificationService.Persistence;
using CryptoBroker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _context;
    private readonly IMapper _mapper;

    public NotificationService(NotificationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<NotificationModel>> GetNotifications(string userId)
    {
        var entities = await _context.Notifications
            .Where(x => x.Order.UserId == userId)
            .OrderBy(x => x.Date)
            .ToListAsync();
        return _mapper.Map<List<NotificationModel>>(entities);
    }

    public async Task<NotificationChannelModel> GetNotificationTypes(string userId, int orderId)
    {
        var entity = await _context.Orders
            .Include(x => x.NotificationChannels)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == orderId);
        return _mapper.Map<NotificationChannelModel>(entity);
    }
}
