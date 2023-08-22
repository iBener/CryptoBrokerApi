using CryptoBroker.Application.Controllers;
using CryptoBroker.BrokerService;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crypto.BrokerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrokerController : BaseController
{
    private readonly IBrokerService _brokerService;

    public BrokerController(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }
    // GET api/<BrokerController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _brokerService.GetOrder(id);
        return result is null ? NotFound() : Ok(result);
    }

    // GET: api/<BrokerController>
    [HttpGet("Search")]
    public async Task<IActionResult> Get(string? userId, string? status)
    {
        var result = await _brokerService.GetOrders(new GetOrdersQueryModel { UserId = userId, Status = status });
        return result is null ? NotFound() : Ok(result);
    }

    // POST api/<BrokerController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderRequestModel value)
    {
        var result = await _brokerService.CreateOrder(value);
        return result is null ? NotFound() : Ok(result);
    }

    // DELETE api/<BrokerController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _brokerService.CancelOrder(id);
        return result is null ? NotFound() : Ok(result);
    }
}
