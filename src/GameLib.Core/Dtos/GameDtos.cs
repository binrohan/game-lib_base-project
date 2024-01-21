namespace GameLib.Core.Dtos;

public record GameToCreateDto(string Title, int PublisherId, IList<int> GenreIds);
public record GameToUpdateDto(string Title, int PublisherId, IList<int> GenreIds);
public record GameToReturnDto(int Id, string Title, PublisherToReturnDto Publisher, IList<GenreToReturnDto> Genres);
public record UpdateGenreDto(int GameId, IList<int> ListofGenreId);
