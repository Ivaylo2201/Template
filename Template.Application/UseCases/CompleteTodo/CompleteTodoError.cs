using System.ComponentModel;

namespace Template.Application.UseCases.CompleteTodo;

public enum CompleteTodoError
{
    [Description("Todo is already completed.")]
    AlreadyCompleted
}