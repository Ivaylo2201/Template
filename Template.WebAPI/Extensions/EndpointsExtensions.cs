using Template.WebAPI.Endpoints;

namespace Template.WebAPI.Extensions;

public static class EndpointsExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGetTodo();
    }
}