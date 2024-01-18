namespace GameLib.Core.Dtos;

public record GenreToCreateDto(string Name);
public record GenreToUpdateDto(string Name);
public record GenreToListDto(int Id, string Name);
