using Microsoft.AspNetCore.Http.HttpResults;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.UpdateTodo;

namespace Template.WebAPI.Endpoints;

public static class UpdateTodo
{
    public static void MapUpdateTodo(this WebApplication app) => app
        .MapPut("todos/{id:int}", UpdateTodoAsync)
        .ProducesValidationProblem() // Include where validation is present, handled by the exception handler
        .WithName(nameof(UpdateTodo));

    private static async Task<Results<NoContent, BadRequest>> UpdateTodoAsync(int id, UpdateTodoRequest request, IUseCase<UpdateTodoRequest, Unit> useCase, CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(request with { Id = id }, ct);
        
        return result.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.BadRequest();
    }
}