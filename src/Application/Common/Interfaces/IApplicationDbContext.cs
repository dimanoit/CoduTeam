using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Project> Projects { get; }
    DbSet<UserProject> UserProjects { get; }
    DbSet<Position> Positions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
