using AutoMapper;
using AutoMapper.Execution;
using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Requests;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Models;

public class OrderModel : IMapFrom<Order>
{
    public virtual int Id { get; set; }

    public virtual string UserId { get; set; } = String.Empty;

    public virtual decimal Amount { get; set; }

    public virtual decimal Price { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public virtual OrderStatus Status { get; set; }

    public virtual List<string>? NotificationChannels { get; set; }

    public virtual DateTime Date { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderModel>()
            //.ForMember(destination => destination.NotificationChannels, member => member.MapFrom(source => Map(source)));
            .ConvertUsing(typeof(CustomOrderOrderModelResolver));

        profile.CreateMap<OrderModel, Order>()
        //.ForMember(destination => destination.NotificationChannels, member => member.MapFrom(source => Map(source)));
            .ConvertUsing(typeof(CustomOrderModelOrderResolver));
    }

    //private static List<string>? Map(Order source)
    //{
    //    var notificationChannels = source.NotificationChannels?.Select(x => ((NotificationType)x.Type).ToString()).ToList();
    //    return notificationChannels;
    //}

    //private static List<NotificationChannel>? Map(OrderModel source)
    //{
    //    var notificationChannels = source.NotificationChannels?.Select(x => new NotificationChannel
    //    {
    //        OrderId = source.Id,
    //        Type = (byte)Enum.Parse<NotificationType>(x)
    //    }).ToList() ?? Enumerable.Empty<NotificationChannel>().ToList();
    //    return notificationChannels;
    //}
}

class CustomOrderOrderModelResolver : ITypeConverter<Order, OrderModel>
{
    public OrderModel Convert(Order source, OrderModel destination, ResolutionContext context) => new()
    {
        Id = source.Id,
        UserId = source.UserId,
        Amount = source.Amount,
        Price = source.Price,
        Date = source.Date,
        Status = (OrderStatus)source.Status,
        NotificationChannels = source.Notifications?.Select(x => ((NotificationType)x.Type).ToString()).ToList()
    };
}

class CustomOrderModelOrderResolver : ITypeConverter<OrderModel, Order>
{
    public Order Convert(OrderModel source, Order destination, ResolutionContext context) => new()
    {
        Id = source.Id,
        UserId = source.UserId,
        Amount = source.Amount,
        Price = source.Price,
        Status = (byte)source.Status,
        Date = source.Date,
        Notifications = source.NotificationChannels?.Select(x => new Notification
        {
            Type = (byte)Enum.Parse<NotificationType>(x)
        }).ToList()
    };
}