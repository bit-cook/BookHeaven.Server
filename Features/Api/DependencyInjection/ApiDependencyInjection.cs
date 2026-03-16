using System.Collections;
using System.Reflection;
using BookHeaven.Server.Features.Api.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nextended.Core.Helper;

namespace BookHeaven.Server.Features.Api.DependencyInjection;

public static class ApiDependencyInjection
{
    private static readonly List<(string basePath, Type interfaceType)> ApiGroups =
    [
        ("/api", typeof(IEndpoint)),
        ("/opds", typeof(IOpdsEndpoint))
    ];
    
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var serviceDescriptors  = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           ApiGroups.Any(group => type.IsAssignableTo(group.interfaceType)))
            .Select(type =>
            {
                var group = ApiGroups.First(group => type.IsAssignableTo(group.interfaceType));
                return ServiceDescriptor.Transient(group.interfaceType, type);
            })
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
    
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app)
    {
        foreach (var group in ApiGroups)
        {
            var groupBuilder = app.MapGroup(group.basePath);
            var mapMethodName = group.interfaceType.GetMethods().First(m => m.Name.StartsWith("Map")).Name;
            
            var endpoints = app.Services.GetServices(group.interfaceType);
            foreach (var endpoint in endpoints)
            {                
                if (endpoint is null) continue;
                var methodInfo = endpoint.GetType().GetMethod(mapMethodName);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(endpoint, [groupBuilder]);
                }
                
            }
        }

        return app;
    }
}