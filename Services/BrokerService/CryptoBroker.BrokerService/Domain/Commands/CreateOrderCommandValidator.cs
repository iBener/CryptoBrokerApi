using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{

    public CreateOrderCommandValidator(BrokerDbContext context)
    {
        RuleFor(x => x.Order.Amount).InclusiveBetween(100, 20_000);
        RuleFor(x => x.Order.UserId).NotNull();
        RuleFor(x => x.Order.Price).GreaterThan(0);
        RuleFor(x => x.Order)
            .Must(order =>
            {
                // TODO: Bu tür "validation" işlerini servisler içinde yapmayı tercih etmeli
                return !context.Orders.Any(x => 
                    x.UserId == order.UserId &&
                    x.Status == (int)OrderStatus.Open);
            })
            .WithMessage("You already have an open order.");
    }
}
