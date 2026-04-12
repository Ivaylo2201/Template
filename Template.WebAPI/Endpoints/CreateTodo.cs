using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.CreateTodo;
using Template.WebAPI.Extensions;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Endpoints;

/// <summary>
/// Use when there is 1 success path and 0 failure paths (excluding validation) AND when you need to access result.Value (Handle null)
/// For validation use .WithValidation which specifies possible ValidationProblemDetails (400)
/// </summary>
public class CreateTodo : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("todos", CreateTodoAsync)
        .WithValidation<CreateTodoRequest>()
        .WithName(nameof(CreateTodo));

    private static async Task<Results<CreatedAtRoute<TodoModel>, ProblemHttpResult>> CreateTodoAsync(
        CreateTodoRequest request,
        IUseCase<CreateTodoRequest, TodoModel> useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(request, ct);
        
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, nameof(GetTodoV1), new { result.Value.Id })
            : TypedResults.Problem(result.ToProblemDetails());
    }
}