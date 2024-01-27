using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Commands.Common;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Positions.Commands.CreatePosition;

public record CreatePositionCommand(
    int ProjectId,
    string Title,
    string Description,
    string ShortDescription,
    bool? IsRemote
) : BaseModifyPositionCommand(Title, Description, ShortDescription, IsRemote), IRequest
{
}

public class CreatePositionCommandHandler(
    IUser user,
    IApplicationDbContext dbContext)
    : IRequestHandler<CreatePositionCommand>
{
    public async Task Handle(CreatePositionCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        Position position = new()
        {
            Title = command.Title,
            Description = command.Description,
            IsRemote = command.IsRemote ?? true,
            ProjectId = command.ProjectId,
            ShortDescription = command.ShortDescription,
        };

        dbContext.Positions.Add(position);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}