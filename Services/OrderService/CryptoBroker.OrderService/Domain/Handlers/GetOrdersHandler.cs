using AutoMapper;
using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Entities;
using CryptoBroker.Models;
using CryptoBroker.Models.Queries;
using CryptoBroker.OrderService.Domain.Queries;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Handlers;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<OrderModel>>
{
    private readonly CryptoDbContext _context;
    private readonly IValidator<GetOrdersQuery> _validator;
    private readonly IMapper _mapper;

    public GetOrdersHandler(CryptoDbContext context, IValidator<GetOrdersQuery> validator, IMapper mapper)
    {
        _context = context;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<List<OrderModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        //if (request.Query.Id > 0)
        //{
        //    var order = await _context.Orders.FindAsync(new object?[] { request.Query.Id }, cancellationToken: cancellationToken);
        //    var orders = new List<Order>();
        //    if (order is not null)
        //    {
        //        orders.Add(order);
        //    }
        //    return _mapper.Map<List<OrderModel>>(orders);
        //}

        IQueryable<Order> query = _context.Orders.AsQueryable();
        if (!String.IsNullOrEmpty(request.Query.UserId))
        {
            query = query.Where(x => x.UserId == request.Query.UserId);
        }
        if (request.Status is not null)
        {
            query = query.Where(x => x.Status == (int)request.Status);
        }

        var orders = await query.ToListAsync(cancellationToken: cancellationToken);
        return _mapper.Map<List<OrderModel>>(orders);
    }
}
