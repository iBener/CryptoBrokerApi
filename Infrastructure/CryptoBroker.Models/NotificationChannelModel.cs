using AutoMapper;
using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Models;

public class NotificationChannelModel : IMapFrom<Order>
{
    public string? UserId { get; set; }

    public int OrderId { get; set; }

    public IEnumerable<string>? NotificationChannels { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, NotificationChannelModel>()
            .ForMember(destination => destination.OrderId, member => member.MapFrom(source => source.Id))
            .ForMember(destination => destination.NotificationChannels, member => member.MapFrom(source => Map(source)));
    }

    private static IEnumerable<string> Map(Order source) 
        => source.NotificationChannels?.Select(x => ((NotificationType)x.Type).ToString()) ?? Enumerable.Empty<string>();
}
