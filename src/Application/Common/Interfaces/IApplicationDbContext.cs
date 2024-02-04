using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Project> Projects { get; }
    DbSet<UserProject> UserProjects { get; }
    DbSet<Position> Positions { get; }
    DbSet<PositionApply> PositionApplies { get; }
    DbSet<UserChat> UserChats { get; }
    DbSet<Domain.Entities.Chat> Chats { get; }
    DbSet<Message> Messages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
