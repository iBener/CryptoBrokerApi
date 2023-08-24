using CryptoBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.EventBus.Commands;

public class NotificationSentCommand
{
    public NotificationModel Notification { get; }

    public NotificationSentCommand(NotificationModel notification)
    {
        Notification = notification;
    }
}
