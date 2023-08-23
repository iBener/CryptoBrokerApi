using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Entities;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; } = String.Empty;

    public decimal Amount { get; set; }

    public decimal Price { get; set; }

    public byte Status { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<NotificationChannel>? NotificationChannels { get; set; }

    public virtual ICollection<Notification>? Notifications { get; set; }
}
