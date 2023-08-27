using CryptoBroker.Application;
using CryptoBroker.Application.Middlewares;
using CryptoBroker.EventBus;
using CryptoBroker.EventBus.Commands;
using CryptoBroker.OrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices<OrderService>("Crypto Orders Api");
builder.Services.AddScoped<IOrderService, OrderService>();

// Event bus
builder.Services.AddApplicationEventBus<OrderService>();

builder.Configuration.AddEnvironmentVariables();
builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateOnBuild = false;
    options.ValidateScopes = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApplicationExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Orders Api v1");
});

app.MapControllers();

app.Run();
