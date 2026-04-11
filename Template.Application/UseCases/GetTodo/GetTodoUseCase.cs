using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.Models;

namespace Template.Application.UseCases.GetTodo;

public class GetTodoUseCase(
    ILogger<GetTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<GetTodoRequest> validator) : IUseCase<GetTodoRequest, TodoModel?>
{
    public async Task<Result<TodoModel?>> ExecuteAsync(GetTodoRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);
        
        var todo = await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .Select(x => new TodoModel(x.Id))
            .FirstOrDefaultAsync(ct);

        return todo is null
            ? (ResultStatus.NotFound, null)
            : (ResultStatus.Ok, todo);
    }
}