using AutoMapper;
using CryptoBroker.Util.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CryptoBroker.Util.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile(Assembly assembly)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    public MappingProfile(List<Assembly> assemblies)
    {
        ApplyMappingsFromAssemblies(assemblies);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var assemblies = ReflectionUtilities.GetReferencedAssemblies(assembly);
        ApplyMappingsFromAssemblies(assemblies);
    }

    private void ApplyMappingsFromAssemblies(List<Assembly> assemblies)
    {
        foreach (var item in assemblies)
        {
            var types = item.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");

                if (methodInfo is null)
                {
                    var intrfc = instance?.GetType().GetInterface("IMapFrom`1");
                    methodInfo = intrfc?.GetMethod("Mapping");
                }

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
