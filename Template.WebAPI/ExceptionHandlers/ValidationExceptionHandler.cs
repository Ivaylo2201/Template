using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Extensions;

namespace Template.WebAPI.ExceptionHandlers;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken ct)
    {
        if (ex is not FluentValidation.ValidationException ve)
            return false;
        
        var errors = ve.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());
        
        logger.ValidationFailed(JsonSerializer.Serialize(errors, Options));
        
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        await ctx.Response.WriteAsJsonAsync(new ValidationProblemDetails(errors), ct);
        
        return true;
    }
}