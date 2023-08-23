using CryptoBroker.Entities;
using CryptoBroker.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Persistence;

[DebuggerStepThrough]
public class BrokerDbContext : EfDbContextBase
{
    public BrokerDbContext() : base(databaseName: "broker")
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<NotificationChannel> NotificationChannels { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");

            entity.Property(e => e.Date).HasColumnType("smalldatetime");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 1)");
        });

        modelBuilder.Entity<NotificationChannel>(entity =>
        {
            entity.HasOne(d => d.Order)
                .WithMany(p => p.NotificationChannels)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NotificationChannel_Order");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.Notifications)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notification_Order");
        });

        base.OnModelCreating(modelBuilder);
    }
}
