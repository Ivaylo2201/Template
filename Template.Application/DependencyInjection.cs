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
                .AddScoped<IWorker<CreateTodoRequest, TodoModel>, CreateTodoWorker>()
                .AddScoped<IWorker<GetTodoRequest, TodoModel?>, GetTodoWorker>()
                .AddScoped<IWorker<CompleteTodoRequest, Unit>, CompleteTodoWorker>();
        }

        private IServiceCollection AddMappers()
        {
            return services
                .AddSingleton(TypeAdapterConfig.GlobalSettings)
                .AddScoped<IMapper, ServiceMapper>();
        }
    }
}