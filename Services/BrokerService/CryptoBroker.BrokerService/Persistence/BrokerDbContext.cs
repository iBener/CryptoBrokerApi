using CryptoBroker.Entities;
using CryptoBroker.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Persistence;

public class BrokerDbContext : EfDbContextBase
{
    public BrokerDbContext() : base(databaseName: "broker")
    {
    }

    public DbSet<Order> Orders { get; set; }
}
