namespace GameLib.Domain.Entities;

public class Genre : AuditableEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Game> Games { get; set; } = [];
    public ICollection<GameGenre> GameGenres { get; set; } = [];
}
