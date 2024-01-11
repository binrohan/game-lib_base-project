namespace GameLib.Domain.Entities;

public class Game : AuditableEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
}
