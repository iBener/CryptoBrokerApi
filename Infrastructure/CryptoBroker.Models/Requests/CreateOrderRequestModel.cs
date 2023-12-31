﻿using AutoMapper;
using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Models.Requests;

public class CreateOrderRequestModel : OrderModel, IMapFrom<Order>
{
    [JsonIgnore]
    public override int Id { get; set; }

    [DefaultValue("")]
    public override string UserId { get; set; } = String.Empty;

    [JsonIgnore]
    public override OrderStatus Status { get; set; }

    [JsonIgnore]
    public override DateTime Date { get; set; }
}
