using AutoMapper;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models;
using CryptoBroker.Models.Enums;
using CryptoBroker.OrderService.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService.Domain.Handlers;

public class GetActiveOrderHandler : IRequestHandler<GetActiveOrderQuery, OrderModel>
{
    private readonly CryptoDbContext _context;
    private readonly IMapper _mapper;

    public GetActiveOrderHandler(CryptoDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrderModel> Handle(GetActiveOrderQuery request, CancellationToken cancellationToken)
    {
        var activeOrder = await _context.Orders
            .FirstOrDefaultAsync(x => x.UserId == request.UserId &&
                                 x.Status == (int)OrderStatus.Open, cancellationToken: cancellationToken);
        return _mapper.Map<OrderModel>(activeOrder);
    }
}
