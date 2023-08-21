using CryptoBroker.BrokerService;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crypto.BrokerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrokerController : ControllerBase
{
    private readonly IBrokerService _brokerService;

    public BrokerController(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    // GET: api/<BrokerController>
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        //return Ok(_context.Orders.ToList());

        throw new NotImplementedException();
    }

    // GET api/<BrokerController>/5
    [HttpGet("{id}")]
    public ActionResult<Order> Get(int id)
    {
        //var order = _context.Orders.Find(id);
        //if (order is null)
        //{
        //    return NotFound();
        //}
        //return Ok(order);

        throw new NotImplementedException();
    }

    // POST api/<BrokerController>
    [HttpPost]
    public ActionResult<Order> Post([FromBody] OrderModel value)
    {
        var result = _brokerService.CreateOrder(value);
        return result is null ? NotFound() : Ok(result);
    }

    // DELETE api/<BrokerController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
