using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.UpdateTodo;

public class UpdateTodoUseCase(
    ILogger<UpdateTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<UpdateTodoRequest> validator) : IUseCase<UpdateTodoRequest, Unit>
{
    public async Task<Result<Unit>> ExecuteAsync(UpdateTodoRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);

        await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.Title, request.Title)
                .SetProperty(x => x.Priority, request.Priority)
                .SetProperty(x => x.IsCompleted, request.IsCompleted), ct);
        
        return (ResultStatus.Ok, Unit.Value);
    }
}