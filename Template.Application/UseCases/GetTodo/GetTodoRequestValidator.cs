using FluentValidation;

namespace Template.Application.UseCases.GetTodo;

public class GetTodoRequestValidator : AbstractValidator<GetTodoRequest>
{
    public GetTodoRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}