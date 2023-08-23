using CryptoBroker.Entities;
using CryptoBroker.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService.Persistence;

[DebuggerStepThrough]
public class OrderDbContext : EfDbContextBase
{
    public OrderDbContext() : base("orders")
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

            entity.Property(e => e.Date).HasColumnType("smalldatetime");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 1)");
        });

        base.OnModelCreating(modelBuilder);
    }
}
