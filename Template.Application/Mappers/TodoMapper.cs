using Mapster;
using Template.Application.Models;
using Template.Domain.Entities;

namespace Template.Application.Mappers;

public class TodoMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Todo, TodoModel>();
    }
}