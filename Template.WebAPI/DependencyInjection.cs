namespace Template.WebAPI;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            return services
                .AddEndpointsApiExplorer()
                .AddOpenApi()
                .AddProblemDetails();
        }
    }
}