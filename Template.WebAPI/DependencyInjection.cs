using Template.WebAPI.Extensions;

namespace Template.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options => options.ConfigureSwaggerGen())
            .AddProblemDetails();
    }
}