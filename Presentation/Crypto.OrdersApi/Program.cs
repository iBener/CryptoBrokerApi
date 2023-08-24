using CryptoBroker.Application;
using CryptoBroker.OrderService;
using CryptoBroker.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddApplicationServices<OrderService>("Crypto Orders Api");
builder.Services.AddScoped<IOrderService, OrderService>();

// Event bus
builder.Services.AddApplicationEventBus<OrderService>(async bus =>
{
    //await bus.Subscribe<OrderNotificationSent>();
    //await bus.Subscribe<UpdateNotificationCompleted>();
});

builder.Configuration.AddEnvironmentVariables();
builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateOnBuild = false;
    options.ValidateScopes = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
