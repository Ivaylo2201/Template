using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;
using Template.WebAPI.Extensions;

namespace Template.WebAPI.Endpoints;

public static class GetTodo
{
    public static void MapGetTodo(this WebApplication app) => app
        .MapGet("todos/{id:int}", GetTodoAsync)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .WithName(nameof(GetTodo));

    private static async Task<Results<Ok<TodoModel>, ProblemHttpResult>> GetTodoAsync(
        [FromRoute] int id,
        IUseCase<GetTodoRequest, TodoModel?> useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(new GetTodoRequest(id), ct);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result.Value)
            : TypedResults.Problem(result.ToProblemDetails());
    }
}