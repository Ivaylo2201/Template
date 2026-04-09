using Template.Core.Enums;

namespace Template.Core.Entities;

public class Todo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
    public required Priority Priority { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}