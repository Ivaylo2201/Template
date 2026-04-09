using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.GetTodo;

namespace Template.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddUseCases();
        }

        private IServiceCollection AddUseCases()
        {
            return services
                .AddScoped<IUseCase<GetTodoRequest, TodoModel?>, GetTodoUseCase>();
        }
    }
}