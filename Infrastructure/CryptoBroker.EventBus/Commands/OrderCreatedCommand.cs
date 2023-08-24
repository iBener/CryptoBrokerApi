using CryptoBroker.Models;

namespace CryptoBroker.EventBus.Commands;

public class OrderCreatedCommand
{
    public OrderModel Order { get; }

    public OrderCreatedCommand(OrderModel order)
    {
        Order = order;
    }
}

public class OrderCreatedNotifyCommand
{
    public OrderModel Order { get; }

    public OrderCreatedNotifyCommand(OrderModel order)
    {
        Order = order;
    }
}