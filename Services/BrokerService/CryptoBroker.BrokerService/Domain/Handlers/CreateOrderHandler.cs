using AutoMapper;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderModel>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderCommand> _validator;

    public CreateOrderHandler(CryptoDbContext context, IMapper mapper, IValidator<CreateOrderCommand> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<OrderModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        // Create order
        var order = _mapper.Map<Order>(request.Order);
        order.Status = (int)OrderStatus.Open;
        order.Date = DateTime.Now;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderModel>(order);
    }
}
