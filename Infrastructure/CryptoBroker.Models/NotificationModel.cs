using AutoMapper;
using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Models;

public class NotificationModel : IMapFrom<Notification>
{
    public int OrderId { get; set; }

    public string NotificationType { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public bool? Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public DateTime? Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationModel>()
            .ForMember(destination => destination.NotificationType, 
                mapper => mapper.MapFrom(source => ((NotificationType)source.Type).ToString()));
    }
}
