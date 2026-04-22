using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Application.UseCases.CompleteTodo;
using Template.Application.UseCases.CreateTodo;
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
                .AddMappers()
                .AddUseCases();
        }

        private IServiceCollection AddUseCases()
        {
            return services
                .AddScoped<IUseCase<CreateTodoRequest, TodoModel>, CreateTodoUseCase>()
                .AddScoped<IUseCase<GetTodoRequest, TodoModel?>, GetTodoUseCase>()
                .AddScoped<IUseCase<CompleteTodoRequest, Unit>, CompleteTodoUseCase>();
        }

        private IServiceCollection AddMappers()
        {
            return services
                .AddSingleton(TypeAdapterConfig.GlobalSettings)
                .AddScoped<IMapper, ServiceMapper>();
        }
    }
}