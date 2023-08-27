using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Models.Enums;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{

    public CreateOrderCommandValidator(CryptoDbContext context)
    {
        RuleFor(x => DateTime.Today.Day).InclusiveBetween(1, 28)
            .WithMessage("You can enter new order between 1st and 28th day of the month.");
        RuleFor(x => x.Order.Amount).InclusiveBetween(100, 20_000);
        RuleFor(x => x.Order.UserId).NotNull().NotEmpty();
        RuleFor(x => x.Order.Price).GreaterThan(0);
        //RuleFor(x => x.Order)
        //    .Must(order =>
        //    {
        //        // TODO: Bu tür "validation" işlerini servisler içinde yapmalı
        //        return !context.Orders.Any(x => 
        //            x.UserId == order.UserId &&
        //            x.Status == (int)OrderStatus.Open);
        //    })
        //    .WithMessage("You already have an open order.");
        //RuleForEach(x => x.NotificationTypes)
        //    .NotNull()
        //    .NotEmpty()
        //    .WithMessage("Please fill at leat one notification channel: Sms, Email, Push (ignore case)");

        RuleFor(x => x.Order)
            .Must(order => !order.NotificationChannels.IsNullOrEmpty())
            .WithMessage("Please fill at leat one notification channel: Sms, Email, Push (ignore case)");
    }
}
