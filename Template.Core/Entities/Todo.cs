using Template.Core.Enums;

namespace Template.Core.Entities;

public class Todo
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public bool IsCompleted { get; set; }
    public required Priority Priority { get; init; }
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
}