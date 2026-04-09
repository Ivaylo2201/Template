using Microsoft.EntityFrameworkCore;
using Template.Core.Entities;

namespace Template.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Todo> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}