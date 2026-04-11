using Template.Core.Enums;

namespace Template.Application.Models;

public record TodoModel(int Id, string Title, bool IsCompleted, Priority Priority, DateTime CreatedAtUtc);