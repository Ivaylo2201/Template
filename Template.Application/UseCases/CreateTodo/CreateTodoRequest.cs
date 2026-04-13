using Template.Core.Enums;

namespace Template.Application.UseCases.CreateTodo;

public record CreateTodoRequest
{
    public required string Title { get; init; }
    public required Priority Priority { get; init; }
}