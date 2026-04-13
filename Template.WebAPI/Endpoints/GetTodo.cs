using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

public class GetTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("todos/{id:int}", GetTodoAsync)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithName(nameof(GetTodo));
    
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