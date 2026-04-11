using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.DeleteTodo;
using Template.WebAPI.Extensions;

namespace Template.WebAPI.Endpoints;

public static class DeleteTodo
{
    public static void MapDeleteTodo(this WebApplication app) => app
        .MapDelete("todos/{id:int}", DeleteTodoAsync)
        .ProducesValidationProblem() // Include where validation is present, handled by the exception handler
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithName(nameof(DeleteTodo));

    private static async Task<Results<NoContent, ProblemHttpResult>> DeleteTodoAsync(int id, IUseCase<DeleteTodoRequest, Unit> useCase, CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(new DeleteTodoRequest(id), ct);

        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.Problem(result.ToProblemDetails());
    }
}