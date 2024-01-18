using GameLib.Domain.ValueObjects;

namespace GameLib.Domain.Entities;

public class Publisher : AuditableEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly? Established { get; set; }
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
    public ICollection<Game> Games { get; set; } = [];
}
