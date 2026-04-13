using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;

namespace Template.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails<TValue>(this Result<TValue> result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert a successful result to ProblemDetails.");
        
        var (status, title) = result.Error switch
        {
            ErrorType.NotFound  => (StatusCodes.Status404NotFound, "Resource not found."),
            ErrorType.Forbidden => (StatusCodes.Status403Forbidden, "Access forbidden."),
            ErrorType.Invalid   => (StatusCodes.Status400BadRequest, "Request validation failed."),
            _                   => (StatusCodes.Status500InternalServerError, "Internal service failure.")
        };

        return new ProblemDetails
        {
            Status = status,
            Title = title
        };
    }
}