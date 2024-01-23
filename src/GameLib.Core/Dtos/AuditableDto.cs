namespace GameLib.Core.Dtos;

public record AuditableDto
{
    public AuditableDto() { }

    public AuditableDto(DateTimeOffset created, int createdBy, DateTimeOffset lastModified, int lastModifiedBy)
    {
        Created = created;
        CreatedBy = createdBy;
        LastModified = lastModified;
        LastModifiedBy = lastModifiedBy;
    }

    public DateTimeOffset Created { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public int LastModifiedBy { get; set; }
}

