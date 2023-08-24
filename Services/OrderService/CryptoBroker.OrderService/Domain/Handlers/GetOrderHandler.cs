using AutoMapper;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models;
using CryptoBroker.OrderService.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService.Domain.Handlers;

public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderModel>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderHandler(CryptoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Orders.FindAsync(new object[] { request.OrderId }, cancellationToken: cancellationToken);
        var model = _mapper.Map<OrderModel>(result);
        return model;
    }
}
