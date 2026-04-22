using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Todo> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}