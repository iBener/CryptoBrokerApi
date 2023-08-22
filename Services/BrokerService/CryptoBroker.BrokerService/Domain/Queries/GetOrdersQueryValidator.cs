﻿using CryptoBroker.BrokerService.Domain.Commands;
using CryptoBroker.Models.Enums;
using CryptoBroker.Models.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.BrokerService.Domain.Queries;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x)
            .Must(q =>
                (q.Query.Id is not null && q.Query.Id != 0) ||
                !String.IsNullOrEmpty(q.Query.UserId) ||
                !String.IsNullOrEmpty(q.Query.Status))
            .WithMessage("You must specifies at least one search criteria.");

        RuleFor(x => x)
            .Must(q => String.IsNullOrEmpty(q.Query.Status) || q.Status is not null)
            .WithMessage($"Order status value should be one of the '{String.Join("', '", Enum.GetValues<OrderStatus>())}' (ignore case).");
    }
}
