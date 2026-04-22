using System.Reflection;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public void MapEndpoints()
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
    }
}