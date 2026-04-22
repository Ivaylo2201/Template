using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;
using Template.Application.Extensions;

namespace Template.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails<TValue>(this Result<TValue> result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Could not convert a successful Result to ProblemDetails.");
        
        return new ProblemDetails { Title = result.ErrorCode.GetDescription() };
    }
}