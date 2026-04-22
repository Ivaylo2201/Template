using Template.Domain.Enums;

namespace Template.Domain.Entities;

public class Todo
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsCompleted { get; set; }
    public Priority Priority { get; init; } = Priority.Medium;
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
}