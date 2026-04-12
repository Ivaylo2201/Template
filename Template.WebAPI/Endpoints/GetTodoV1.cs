using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

/// <summary>
/// Version 1:
/// - Use this when the use case has 1 success path and 1 failure path
/// - Use the correct response type paired with ProblemDetails
/// </summary>
public class GetTodoV1 : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("todos/v1/{id:int}", GetTodoAsync)
        .WithName(nameof(GetTodoV1));
    
    private static async Task<Results<Ok<TodoModel>, NotFound<ProblemDetails>>> GetTodoAsync(
        int id,
        IUseCase<GetTodoRequest, TodoModel?> useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(new GetTodoRequest(id), ct);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ToProblemDetails());
    }
}