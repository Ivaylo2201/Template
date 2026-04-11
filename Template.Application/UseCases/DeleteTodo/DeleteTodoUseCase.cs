using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Extensions;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.DeleteTodo;

public class DeleteTodoUseCase(
    ILogger<DeleteTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<DeleteTodoRequest> validator) : IUseCase<DeleteTodoRequest, Unit>
{
    public async Task<Result<Unit>> ExecuteAsync(DeleteTodoRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);
        
        var rows = await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(ct);

        if (rows is 0)
        {
            logger.TodoNotFound(request.Id);
            return ResultStatus.NotFound;
        }

        return (ResultStatus.Ok, Unit.Value);
    }
}