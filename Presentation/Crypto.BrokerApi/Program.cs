using Crypto.BrokerApi.BackgroundServices;
using CryptoBroker.Application;
using CryptoBroker.Application.Middlewares;
using CryptoBroker.BrokerService;
using CryptoBroker.EventBus;
using CryptoBroker.EventBus.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices<BrokerService>("Crypto Broker Api");
builder.Services.AddScoped<IBrokerService, BrokerService>();

// Event bus
builder.Services.AddApplicationEventBus<BrokerService>();

// Test amaçlý
//builder.Services.AddHostedService<OrderUpdateBackgroundService>();

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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Broker Api v1");
});

app.MapControllers();

app.Run();
