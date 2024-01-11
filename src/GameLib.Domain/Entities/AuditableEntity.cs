namespace GameLib.Domain.Entities;

public class AuditableEntity
{
    public DateTimeOffset Created { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public int LastModifiedBy { get; set; }
}
