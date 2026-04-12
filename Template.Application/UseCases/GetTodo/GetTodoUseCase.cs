using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Extensions;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Core.Entities;

namespace Template.Application.UseCases.GetTodo;

public class GetTodoUseCase(
    ILogger<GetTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<GetTodoRequest> validator,
    IMapper<Todo, TodoModel> mapper) : IUseCase<GetTodoRequest, TodoModel?>
{
    public async Task<Result<TodoModel?>> ExecuteAsync(GetTodoRequest request, CancellationToken ct)
    {
        var result = await validator.ValidateAsync(request, ct);

        if (!result.IsValid)
        {
            logger.ValidationFailed(result.Errors.Select(x => x.ErrorMessage));
            return Result<TodoModel?>.Failure(ErrorType.Invalid);
        }
        
        var todo = await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .Select(x => mapper.Map(x))
            .FirstOrDefaultAsync(ct);

        if (todo is null)
        {
            logger.TodoNotFound(request.Id);
            return Result<TodoModel?>.Failure(ErrorType.NotFound);
        }
        
        return Result<TodoModel?>.Success(todo);
    }
}