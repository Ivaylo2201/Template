using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Extensions;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.CompleteTodo;

public class CompleteTodoUseCase(
    ILogger<CompleteTodoUseCase> logger,
    IAppDbContext dbContext) : IUseCase<CompleteTodoRequest, Unit>
{
    public async Task<Result<Unit>> ExecuteAsync(CompleteTodoRequest request, CancellationToken ct)
    {
        var todo = await dbContext.Todos.SingleOrDefaultAsync(x => x.Id == request.TodoId, ct);

        if (todo is null)
        {
            logger.TodoNotFound(request.TodoId);
            return Result<Unit>.Failure(Error.NotFound);
        }

        if (todo.IsCompleted)
        {
            logger.TodoAlreadyCompleted(request.TodoId);
            return Result<Unit>.Failure(CompleteTodoError.AlreadyCompleted);
        }

        todo.IsCompleted = true;
        await dbContext.SaveChangesAsync(ct);
        
        return Result<Unit>.Success(Unit.Value);
    }
}