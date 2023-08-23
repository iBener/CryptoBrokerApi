﻿using CryptoBroker.Util.Mappings;
using CryptoBroker.Util.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoBroker.Application;

public static class ApplicationExtensions
{

    public static IServiceCollection AddApplicationServices<TService>(this IServiceCollection services, string apiTitle)
    {
        var assembly = typeof(TService).Assembly;
        var assemblies = ReflectionUtilities.GetReferencedAssemblies(assembly);

        // Automapper
        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile(assemblies));
        });
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        // FluentValidation
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddControllers()
            .AddJsonOptions(options =>
               options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = apiTitle, Version = "v1" });
        });

        return services;
    }

    public static IServiceCollection AddApplicationEventBus<T>(this IServiceCollection services, Func<Rebus.Bus.IBus, Task>? onCreated = null)
    {
        services.AddRebus(rebus => rebus
            .Routing(r =>
                r.TypeBased().MapAssemblyOf<T>($"crypto-que"))
            .Transport(t =>
                t.UseRabbitMq(connectionString: "amqp://crypto.rabbitmq:5672", $"crypto-que"))
            //.Sagas(s => s.StoreInMemory())
            , onCreated: onCreated);

        services.AutoRegisterHandlersFromAssemblyOf<T>();

        return services!;
    }
}

public record ApplicationInfo(string Name, string ApiVersion);