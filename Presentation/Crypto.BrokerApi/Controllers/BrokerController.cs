using CryptoBroker.Application.Controllers;
using CryptoBroker.BrokerService;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models.Requests;
using Microsoft.AspNetCore.Mvc;

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

    // POST api/<BrokerController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderRequestModel value)
    {
        var result = await _brokerService.CreateOrder(value);
        return result is null ? NotFound() : Ok(result);
    }

    // DELETE api/<BrokerController>/5
    [HttpDelete("{userId}/{orderId:int}")]
    public async Task<IActionResult> Delete(string userId, int orderId)
    {
        var result = await _brokerService.CancelOrder(userId, orderId);
        return result is null ? NotFound() : Ok(result);
    }
}
