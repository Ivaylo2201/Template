using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces;
using Template.Application.Mappers;
using Template.Application.Models;
using Template.Application.UseCases.CreateTodo;
using Template.Application.UseCases.GetTodo;
using Template.Core.Entities;

namespace Template.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddUseCases()
                .AddMappers();
        }

        private IServiceCollection AddUseCases()
        {
            return services
                .AddScoped<IUseCase<GetTodoRequest, TodoModel?>, GetTodoUseCase>()
                .AddScoped<IUseCase<CreateTodoRequest, TodoModel?>, CreateTodoUseCase>();
        }

        private IServiceCollection AddMappers()
        {
            return services
                .AddTransient<IMapper<Todo, TodoModel>, TodoMapper>();
        }
    }
}