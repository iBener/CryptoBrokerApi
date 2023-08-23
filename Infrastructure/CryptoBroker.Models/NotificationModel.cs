using CryptoBroker.Entities;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Models;

public class NotificationModel : IMapFrom<Notification>
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public byte Type { get; set; }

    public bool Success { get; set; }

    public DateTime? Date { get; set; }
}
