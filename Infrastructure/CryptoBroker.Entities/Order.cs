using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Entities;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int Amount { get; set; }

    public int Price { get; set; }

    public string NotificationType { get; set; }
}
