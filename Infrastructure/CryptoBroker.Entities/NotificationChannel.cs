using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Entities;

public class NotificationChannel : BaseEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public byte Type { get; set; }

    public virtual Order Order { get; set; } = default!;
}
