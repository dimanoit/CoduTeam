namespace CoduTeam.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public int? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public int? LastModifiedBy { get; set; }
}
