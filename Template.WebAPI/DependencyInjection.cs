using Template.WebAPI.Extensions;

namespace Template.WebAPI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            return services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(options => options.ConfigureSwagger())
                .AddProblemDetails();
        }
    }
}