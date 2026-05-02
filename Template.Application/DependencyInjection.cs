using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.CompleteTodo;

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