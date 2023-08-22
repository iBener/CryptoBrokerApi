using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Application.Controllers;

public class BaseController : ControllerBase
{
    public Task<IActionResult> Result<T>(T result)
    {
        IActionResult actionResult = result is null ? NotFound() : Ok(result);

        return Task.FromResult(actionResult);
    }
}
