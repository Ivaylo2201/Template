using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.CompleteTodo;
using Template.Application.UseCases.ComplexUseCase;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

public class CompleteTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPatch("todos/{todoId:int}/complete", CompleteTodoAsync)
        .WithName(nameof(CompleteTodo));

    private static async Task<Results<NoContent, NotFound<ProblemDetails>, Conflict<ProblemDetails>>> CompleteTodoAsync(
        [FromRoute] int todoId,
        [FromServices] IWorker<ComplexUseCaseRequest, ComplexUseCaseResponse> worker,
        CancellationToken ct)
    {
        var result = await worker.ExecuteAsync(new ComplexUseCaseRequest(), ct);

        if (result.IsSuccess)
            return TypedResults.NoContent();

        return result.ErrorCode switch
        {
            CompleteTodoError.AlreadyCompleted => TypedResults.Conflict(result.ToProblemDetails()),
            _ => TypedResults.NotFound(result.ToProblemDetails())
        };
    } 
}