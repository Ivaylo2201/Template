using FluentValidation;

namespace Template.Application.UseCases.CreateTodo;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
    }
}