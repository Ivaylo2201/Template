using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;
using Template.WebAPI.Extensions;

namespace Template.WebAPI.Endpoints;

public static class GetTodo
{
    public static void MapGetTodo(this WebApplication app) => app
        .MapGet("todos/{id:int}", GetTodoAsync)
        .ProducesValidationProblem() // Include where validation is present, handled by the exception handler
        .ProducesProblem(StatusCodes.Status404NotFound) // Include for each non-success Result<T> case
        .WithName(nameof(GetTodo));

    private static async Task<Results<Ok<TodoModel>, ProblemHttpResult>> GetTodoAsync(int id, IUseCase<GetTodoRequest, TodoModel?> useCase, CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(new GetTodoRequest(id), ct);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result.Value)
            : TypedResults.Problem(result.ToProblemDetails());
    }
}