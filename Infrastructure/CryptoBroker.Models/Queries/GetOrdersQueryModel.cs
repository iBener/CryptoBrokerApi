using CryptoBroker.Entities;
using CryptoBroker.Models.Enums;
using CryptoBroker.Util.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Models.Queries;

public class GetOrdersQueryModel
{
    public string? UserId { get; set; }

    public string? Status { get; set; }
}
