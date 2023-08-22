using AutoMapper;
using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.BrokerService.Domain.Queries;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderModel>
{
    private readonly BrokerDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderHandler(BrokerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Orders.FindAsync(request.OrderId, cancellationToken);
        return _mapper.Map<OrderModel>(result);
    }
}
