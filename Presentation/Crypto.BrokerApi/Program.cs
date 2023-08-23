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
builder.Services.AddApplicationServices<BrokerService>("Crypto Broker Api");
builder.Services.AddScoped<IBrokerService, BrokerService>();
builder.Services.AddDbContext<BrokerDbContext>();

// Event bus
builder.Services.AddApplicationEventBus<BrokerService>(async bus =>
{
    await bus.Subscribe<OrderNotificationSent>();
    await bus.Subscribe<UpdateNotificationCompleted>();
});

// Test amaçlý
builder.Services.AddHostedService<OrderUpdateBackgroundService>();

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
