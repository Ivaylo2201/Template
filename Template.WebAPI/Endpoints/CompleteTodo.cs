using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.CompleteTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

public class CompleteTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder) => builder
        .MapPatch("/v1/todos/{todoId:int}/complete", CompleteTodoAsync)
        .WithDescription("Marks a Todo record as completed if not already completed.");

    private static async Task<Results<NoContent, NotFound<ProblemDetails>, Conflict<ProblemDetails>>> CompleteTodoAsync(
        [FromRoute] int todoId,
        [FromServices] IWorker<CompleteTodoRequest, Unit> worker,
        [FromHeader(Name = "Request-Id")] string requestId,
        CancellationToken ct)
    {
        var result = await worker.ProcessAsync(new CompleteTodoRequest(todoId), ct);

        if (result.IsSuccess)
            return TypedResults.NoContent();

        return result.ErrorCode switch
        {
            CompleteTodoError.AlreadyCompleted => TypedResults.Conflict(result.ToProblemDetails()),
            _ => TypedResults.NotFound(result.ToProblemDetails())
        };
    }
}