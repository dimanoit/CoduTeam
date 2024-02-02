using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using CoduTeam.Domain.Events;

namespace CoduTeam.Application.Projects.EventHandlers;

public class PositionApplyStatusChangedEventHandler(IApplicationDbContext dbContext)
    : INotificationHandler<PositionApplyStatusChangedEvent>
{
    public async Task Handle(
        PositionApplyStatusChangedEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.PositionApply.Status == PositionApplyStatus.Confirmed)
        {
            UserProject accountProject = new()
            {
                UserId = notification.PositionApply!.UserId,
                Project = notification.PositionApply.Position!.Project
            };
            
            dbContext.UserProjects.Add(accountProject);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
