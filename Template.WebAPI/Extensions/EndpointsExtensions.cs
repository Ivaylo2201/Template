using System.Reflection;
using Template.WebAPI.Filters;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Extensions;

public static class EndpointsExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = typeof(IEndpoint).Assembly
            .GetTypes()
            .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && x is { IsAbstract: false, IsInterface: false });

        foreach (var type in endpoints)
        {
            var method = type.GetMethod(nameof(IEndpoint.Map), BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, [app]);
        }
    }
    
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}