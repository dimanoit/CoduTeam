using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Commands;

public record UpdateProjectCommand(int Id,
    string Title,
    string Description,
    Category? Category,
    Country? Country,
    string? ProjectImgUrl) : IRequest;
public class UpdateProjectCommandHandler :IRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.FindAsync( new object[] {request.Id} ,cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Category = request.Category;
        entity.Country = request.Country;
        entity.ProjectImageUrl = request.ProjectImgUrl;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
    
