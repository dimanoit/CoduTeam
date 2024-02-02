namespace CoduTeam.Domain.Events;

public class PositionApplyStatusChangedEvent(PositionApply positionApply) : BaseEvent
{
    public PositionApply PositionApply => positionApply;
}
