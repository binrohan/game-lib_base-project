namespace GameLib.Core.Dtos;

public record GenreToCreateDto(string Name);
public record GenreToUpdateDto(string Name);
public record GenreToReturnDto(int Id, string Name);
