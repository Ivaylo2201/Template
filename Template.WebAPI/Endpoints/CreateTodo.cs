using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.CreateTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

public class CreateTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("todos", CreateTodoAsync)
        .WithName(nameof(CreateTodo));

    private static async Task<Results<CreatedAtRoute<TodoModel>, ProblemHttpResult>> CreateTodoAsync(
        CreateTodoRequest request,
        IWorker<CreateTodoRequest, TodoModel> worker,
        CancellationToken ct)
    {
        var result = await worker.ExecuteAsync(request, ct);
        
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, nameof(GetTodo), new { result.Value.Id })
            : TypedResults.Problem(result.ToProblemDetails());
    } 
}