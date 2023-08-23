using AutoMapper;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, OrderModel>
{
    private readonly BrokerDbContext _context;
    private readonly IMapper _mapper;

    public CancelOrderHandler(BrokerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderModel> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId && x.UserId == request.UserId, cancellationToken: cancellationToken);
        if (order != null)
        {
            order.Status = (int)OrderStatus.Cancelled;
            _context.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return _mapper.Map<OrderModel>(order);
    }
}
