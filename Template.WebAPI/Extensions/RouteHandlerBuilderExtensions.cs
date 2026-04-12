using Template.WebAPI.Filters;

namespace Template.WebAPI.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}