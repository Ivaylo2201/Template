using Template.Domain.Enums;

namespace Template.Application.Models;

public class TodoModel
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required bool IsCompleted { get; init; }
    public required Priority Priority { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
}