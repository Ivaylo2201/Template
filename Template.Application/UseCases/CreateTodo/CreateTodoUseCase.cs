using FluentValidation;
using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Core.Entities;

namespace Template.Application.UseCases.CreateTodo;

public class CreateTodoUseCase(
    ILogger<CreateTodoUseCase> logger,
    IAppDbContext dbContext,
    IValidator<CreateTodoRequest> validator,
    IMapper<Todo, TodoModel> mapper) : IUseCase<CreateTodoRequest, TodoModel?>
{
    public async Task<Result<TodoModel?>> ExecuteAsync(CreateTodoRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);
        
        var todo = new Todo
        {
            Title = request.Title,
            Priority = request.Priority
        };
        
        await dbContext.Todos.AddAsync(todo, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return Result<TodoModel?>.Success(ResultStatus.Created, mapper.Map(todo));
    }
}