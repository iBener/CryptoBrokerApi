using CryptoBroker.Application.Controllers;
using CryptoBroker.BrokerService;
using CryptoBroker.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.BrokerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET api/<BrokerController>/5
    [HttpGet("ActiveOrder/{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var result = await _orderService.GetActiveOrder(userId);
        return result is null ? NotFound() : Ok(result);
    }

    // GET api/<BrokerController>/5
    [HttpGet("{userId}/{orderId}")]
    public async Task<IActionResult> Get(string userId, int orderId)
    {
        var result = await _orderService.GetOrder(userId, orderId);
        return result is null ? NotFound() : Ok(result);
    }

    // GET: api/<BrokerController>
    [HttpGet("Search")]
    public async Task<IActionResult> Get(string? userId, string? status)
    {
        var result = await _orderService.GetOrders(new GetOrdersQueryModel { UserId = userId, Status = status });
        return result is null ? NotFound() : Ok(result);
    }
}
