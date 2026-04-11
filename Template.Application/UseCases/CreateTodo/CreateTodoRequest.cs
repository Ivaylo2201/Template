using Template.Core.Enums;

namespace Template.Application.UseCases.CreateTodo;

public record CreateTodoRequest(string Title, Priority Priority);