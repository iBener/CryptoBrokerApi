using CryptoBroker.Entities;
using CryptoBroker.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.NotificationService.Persistence;

[DebuggerStepThrough]
public class NotificationDbContext : EfDbContextBase
{
    public NotificationDbContext() : base("notifications")
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("smalldatetime");
        });

        base.OnModelCreating(modelBuilder);
    }
}
