using CryptoBroker.BrokerService.Persistence;
using CryptoBroker.Util.Mappings;
using CryptoBroker.Util.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CryptoBroker.Application;

public static class ApplicationExtensions
{

    public static IServiceCollection AddApplicationServices<TService>(this IServiceCollection services, string apiTitle)
    {
        var assembly = typeof(TService).Assembly;
        var assemblies = ReflectionUtilities.GetReferencedAssemblies(assembly);

        // DbContext
        services.AddDbContext<CryptoDbContext>();

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
}

public record ApplicationInfo(string Name, string ApiVersion);