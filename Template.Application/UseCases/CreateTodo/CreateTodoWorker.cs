using MapsterMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Domain.Entities;

namespace Template.Application.UseCases.CreateTodo;

public class CreateTodoWorker(
    ILogger<CreateTodoWorker> logger,
    IAppDbContext dbContext,
    IMapper mapper) : IWorker<CreateTodoRequest, TodoModel>
{
    public async Task<Result<TodoModel>> ExecuteAsync(CreateTodoRequest request, CancellationToken ct)
    {
        var todo = new Todo
        {
            Title = request.Title,
            Priority = request.Priority
        };
        
        await dbContext.Todos.AddAsync(todo, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return Result<TodoModel>.Success(mapper.Map<TodoModel>(todo));
    }
}