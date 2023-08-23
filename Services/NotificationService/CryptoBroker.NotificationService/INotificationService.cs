using CryptoBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService;

public interface INotificationService
{
    Task<List<NotificationModel>> GetNotifications(string userId);
    Task<NotificationChannelModel> GetNotificationTypes(string userId, int orderId);
}
