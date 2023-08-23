using CryptoBroker.Application.Controllers;
using CryptoBroker.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.NotificationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : BaseController
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // GET api/<OrderController>/5
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var result = await _notificationService.GetNotifications(userId);
        return result is null ? NotFound() : Ok(result);
    }

    // GET api/<OrderController>/5
    [HttpGet("Channels/{userId}/{orderId:int}")]
    public async Task<IActionResult> Get(string userId, int orderId)
    {
        var result = await _notificationService.GetNotificationTypes(userId, orderId);
        return result is null ? NotFound() : Ok(result);
    }
}
