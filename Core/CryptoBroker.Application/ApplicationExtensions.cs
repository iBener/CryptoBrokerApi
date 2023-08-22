using CryptoBroker.Util.Mappings;
using CryptoBroker.Util.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Application;

public static class ApplicationExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Assembly assembly)
    {
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

        return services;
    }
}
