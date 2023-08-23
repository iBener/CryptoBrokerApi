using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Models.Enums;

public enum OrderStatus : byte
{
    New,
    Open,
    Filled,
    Cancelled
}
