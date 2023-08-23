using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService.Domain.Queries;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.Query.UserId).NotNull().NotEmpty();

        //RuleFor(x => x)
        //    .Must(q =>
        //        !String.IsNullOrEmpty(q.Query.UserId) ||
        //        !String.IsNullOrEmpty(q.Query.Status))
        //    .WithMessage("You must specifies at least one search criteria.");

        RuleFor(x => x)
            .Must(q => string.IsNullOrEmpty(q.Query.Status) || q.Status is not null)
            .WithMessage($"Order status value should be one of the '{string.Join("', '", Enum.GetValues<OrderStatus>())}' (ignore case).");
    }
}
