using Crypto.BrokerApi;
using Crypto.BrokerApi.BackgroundServices;
using CryptoBroker.Application;
using CryptoBroker.Application.Middlewares;
using CryptoBroker.BrokerService;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using System;
using System.ComponentModel;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(BrokerService).Assembly);
});

builder.Services.AddScoped<IBrokerService, BrokerService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crypto Broker Api", Version = "v1" });
});

builder.Services.AddDbContext<BrokerDbContext>();

// Test amaçlý
builder.Services.AddHostedService<OrderUpdateBackgroundService>();

// Event bus
builder.Services.AddApplicationEventBus<BrokerService>(async bus =>
{
    await bus.Subscribe<OrderNotificationSent>();
    await bus.Subscribe<UpdateNotificationCompleted>();
});

//builder.Services.AddRebus(rebus => rebus
//            .Routing(r =>
//                r.TypeBased().MapAssemblyOf<BrokerService>($"crypto-que"))
//            .Transport(t =>
//                t.UseRabbitMq(connectionString: "amqp://rabbitmq:5672", $"crypto-que"))
//            .Sagas(s => s.StoreInMemory()), onCreated: async bus =>
//            {
//                await bus.Subscribe<OrderNotificationSent>();
//                await bus.Subscribe<UpdateNotificationCompleted>();
//            });
//builder.Services.AutoRegisterHandlersFromAssemblyOf<BrokerService>();

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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
