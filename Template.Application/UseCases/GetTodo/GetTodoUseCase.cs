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
    IMapper<Todo, TodoModel> mapper) : IUseCase<GetTodoRequest, TodoModel?>
{
    public async Task<Result<TodoModel?>> ExecuteAsync(GetTodoRequest request, CancellationToken ct)
    {
        var todo = await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .Select(x => mapper.Map(x))
            .FirstOrDefaultAsync(ct);

        if (todo is null)
        {
            logger.TodoNotFound(request.Id);
            return Result<TodoModel?>.Failure(ResultStatus.NotFound);
        }
        
        return Result<TodoModel?>.Success(ResultStatus.Ok, todo);
    }
}