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

    public virtual int Amount { get; set; }

    public virtual int Price { get; set; }

    public virtual string NotificationType { get; set; } = String.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public virtual OrderStatus Status { get; set; }

    public virtual DateTime Date { get; set; }
}
