using FluentValidation;
using Template.Application.Extensions;

namespace Template.WebAPI.Filters;

public class ValidationFilter<TRequest>(
    ILogger<ValidationFilter<TRequest>> logger,
    IValidator<TRequest> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (request is not null)
        {
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());
            
                logger.ValidationFailed(errors.Values.SelectMany(x => x));
            
                return Results.ValidationProblem(errors);
            }
        }
        
        return await next(context);
    }
}