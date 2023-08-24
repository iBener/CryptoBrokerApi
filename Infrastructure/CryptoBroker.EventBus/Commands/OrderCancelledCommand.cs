using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.EventBus.Commands;

public class OrderCancelledCommand
{
    public int OrderId { get; }

    public OrderCancelledCommand(int orderId)
    {
        OrderId = orderId;
    }
}

public class OrderCancelledNotifyCommand
{
    public int OrderId { get; }

    public OrderCancelledNotifyCommand(int orderId)
    {
        OrderId = orderId;
    }
}
