using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Application.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(ex, httpContext);
        }
    }

    private static Task HandleException(Exception ex, HttpContext httpContext)
    {
        var result = JsonSerializer.Serialize(new
        {
            ex.Message
        });
        httpContext.Response.StatusCode = 400;
        httpContext.Response.ContentType = "application/json";
        return httpContext.Response.WriteAsync(result);
    }
}

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandler>();
    }
}
