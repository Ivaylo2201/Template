using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Extensions;
using Template.Application.Interfaces;
using Template.Application.Models;

namespace Template.Application.UseCases.GetTodo;

public class GetTodoWorker(
    ILogger<GetTodoWorker> logger,
    IAppDbContext dbContext) : IWorker<GetTodoRequest, TodoModel?>
{
    public async Task<Result<TodoModel?>> ProcessAsync(GetTodoRequest request, CancellationToken ct)
    {
        var todo = await dbContext.Todos
            .Where(x => x.Id == request.Id)
            .ProjectToType<TodoModel>()
            .FirstOrDefaultAsync(ct);
        
        if (todo is null)
        {
            logger.TodoNotFound(request.Id);
            return Result<TodoModel?>.Failure(Error.NotFound);
        }

        return Result<TodoModel?>.Success(todo);
    }
}