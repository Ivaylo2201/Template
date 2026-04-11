using Template.Application.Interfaces;
using Template.Application.Models;
using Template.Core.Entities;

namespace Template.Application.Mappers;

public class TodoMapper : IMapper<Todo, TodoModel>
{
    public TodoModel Map(Todo source) => new(source.Id, source.Title, source.IsCompleted, source.Priority, source.CreatedAtUtc);
}