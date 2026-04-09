using Microsoft.EntityFrameworkCore;
using Template.Application.Interfaces;
using Template.Core.Entities;

namespace Template.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Todo> Todos => Set<Todo>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}