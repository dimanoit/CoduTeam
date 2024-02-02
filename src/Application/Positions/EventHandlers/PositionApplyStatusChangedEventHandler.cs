using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Enums;
using CoduTeam.Domain.Events;

namespace CoduTeam.Application.Positions.EventHandlers;

public class PositionApplyStatusChangedEventHandler(IApplicationDbContext dbContext)
    : INotificationHandler<PositionApplyStatusChangedEvent>
{
    public async Task Handle(
        PositionApplyStatusChangedEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.PositionApply.Status == PositionApplyStatus.Confirmed)
        {
            notification.PositionApply.Position!.PositionStatus = PositionStatus.Closed;

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
