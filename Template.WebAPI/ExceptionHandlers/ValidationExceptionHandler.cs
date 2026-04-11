using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Template.WebAPI.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken ct)
    {
        if (ex is not FluentValidation.ValidationException ve)
            return false;
        
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        
        var errors = ve.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());
        
        await ctx.Response.WriteAsJsonAsync(new ValidationProblemDetails(errors), ct);
        return true;
    }
}