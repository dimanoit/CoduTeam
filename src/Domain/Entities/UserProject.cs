namespace CoduTeam.Domain.Entities;

public class UserProject: BaseAuditableEntity
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}
