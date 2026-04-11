using Template.Core.Enums;

namespace Template.Application.UseCases.UpdateTodo;

// Id is set using "with" with the route value
public record UpdateTodoRequest(int Id, string Title, Priority Priority, bool IsCompleted);