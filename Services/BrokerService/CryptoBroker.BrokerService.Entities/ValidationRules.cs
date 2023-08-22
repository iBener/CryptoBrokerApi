using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Entities;

public class ValidationRules
{
    public int Id { get; set; }

    public int MinAmount { get; set; }

    public int MaxAmount { get; set; }
}
