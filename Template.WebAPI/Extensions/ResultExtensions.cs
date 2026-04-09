using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;

namespace Template.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails<TValue>(this Result<TValue> result)
    {
        (int Status, string Title) x = result.Status switch
        {
            ResultStatus.NotFound => (StatusCodes.Status404NotFound, "Resource not found."),
            ResultStatus.Forbidden => (StatusCodes.Status403Forbidden, "Access forbidden."),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error.")
        };
        
        return new ProblemDetails
        {
            Status = x.Status,
            Title = x.Title
        };
    }
}