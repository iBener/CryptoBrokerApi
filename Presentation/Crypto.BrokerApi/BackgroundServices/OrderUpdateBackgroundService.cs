using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Crypto.BrokerApi.BackgroundServices;

/// <summary>
/// Bu servis normalde projenin bir parçası değildir.
/// Sistemin çalıştığının daha sağlıklı test edilebilmesi için oluşturulmuştur.
/// </summary>
public class OrderUpdateBackgroundService : BackgroundService
{
    private readonly CryptoDbContext _context;

    public OrderUpdateBackgroundService(CryptoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Test amaçlı belli bir süre sonra emirleri tamamlandı durumuna çeker
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const int waitSeconds = 300;

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5000, stoppingToken);

            // !!! In-memory databse'de ExecuteSql metodu çalışmıyor
            //_context.Database.ExecuteSql($"update Orders set Status = 2 where Status = 1 and Date <= dateadd(s, -30, getdate())");

            var allOpenOrders = _context.Orders.Where(x => x.Status == (int)OrderStatus.Open && x.Date < DateTime.Now.AddSeconds(-waitSeconds)).ToList();
            if (allOpenOrders?.Any() ?? false)
            {
                foreach (var order in allOpenOrders)
                {
                    order.Status = (int)OrderStatus.Filled;
                    _context.Update(order);
                }
                await _context.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
