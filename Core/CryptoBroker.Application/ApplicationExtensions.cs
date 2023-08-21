using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Application;

public static class ApplicationExtensions
{

    public static IServiceCollection AddMediatR(this IServiceCollection services, Type app)
    {


        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(app.Assembly);
        });

        return services;
    }
}
