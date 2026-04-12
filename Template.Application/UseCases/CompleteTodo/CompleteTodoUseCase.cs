using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Extensions;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.CompleteTodo;

public class CompleteTodoUseCase(
    ILogger<CompleteTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<CompleteTodoRequest> validator) : IUseCase<CompleteTodoRequest, Unit>
{
    public async Task<Result<Unit>> ExecuteAsync(CompleteTodoRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);
        
        var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (todo is null)
        {
            logger.TodoNotFound(request.Id);
            return Result<Unit>.Failure(ResultStatus.NotFound);
        }
        
        todo.IsCompleted = true;
        await dbContext.SaveChangesAsync(ct);
        
        return Result<Unit>.Success(ResultStatus.Ok, Unit.Value);
    }
}