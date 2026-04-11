using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;

namespace Template.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails<TValue>(this Result<TValue> result)
    {
        var (status, title) = result.Status switch
        {
            ResultStatus.NotFound  => (StatusCodes.Status404NotFound, "Resource not found."),
            ResultStatus.Forbidden => (StatusCodes.Status403Forbidden, "Access forbidden."),
            ResultStatus.Invalid   => (StatusCodes.Status400BadRequest, "Request validation failed."),
            _                      => (StatusCodes.Status500InternalServerError, "Internal service failure.")
        };

        return new ProblemDetails
        {
            Status = status,
            Title = title
        };
    }
}