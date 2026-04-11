using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.CreateTodo;

namespace Template.WebAPI.Endpoints;

public static class CreateTodo
{
    public static void MapCreateTodo(this WebApplication app) => app
        .MapPost("todos", CreateTodoAsync)
        .ProducesValidationProblem() // Include where validation is present, handled by the exception handler
        .WithName(nameof(CreateTodo));

    private static async Task<Results<CreatedAtRoute<TodoModel>, BadRequest>> CreateTodoAsync(CreateTodoRequest request, IUseCase<CreateTodoRequest, TodoModel> useCase, CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(request, ct);
        
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, nameof(GetTodo), new { result.Value.Id })
            : TypedResults.BadRequest();
    }
}