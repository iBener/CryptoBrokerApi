using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Entities;

/// <summary>
/// Notification DB Entity.
/// </summary>
public class Notification
{
    public int Id { get; set; }

    public int OrderId { get; set; }
}
