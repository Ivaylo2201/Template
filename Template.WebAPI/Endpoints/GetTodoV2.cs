using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

/// <summary>
/// Version 2:
/// - Use this when the use case has 1 success path and N failure paths
/// - Define a .ProducesProblem() for each failure response
/// </summary>
public class GetTodoV2 : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("todos/v2/{id:int}", GetTodoAsync)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName(nameof(GetTodoV2));
    
    private static async Task<Results<Ok<TodoModel>, ProblemHttpResult>> GetTodoAsync(
        int id,
        IUseCase<GetTodoRequest, TodoModel?> useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(new GetTodoRequest(id), ct);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result.Value)
            : TypedResults.Problem(result.ToProblemDetails());
    }
}