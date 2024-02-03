using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Project> Projects { get; }
    DbSet<UserProject> UserProjects { get; }
    DbSet<Position> Positions { get; }
    DbSet<Message> Messages { get; }
    DbSet<Domain.Entities.Chat> Chats { get; }
    DbSet<UserChat> UserChats { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
