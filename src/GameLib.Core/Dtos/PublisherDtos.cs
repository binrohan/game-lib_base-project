namespace GameLib.Core.Dtos;

public record PublisherToCreateDto(string Name, DateOnly? Established, IEnumerable<PhoneNumberDto>? PhoneNumbers);
public record PublisherToListDto(int Id, string Name, DateOnly? Established, IEnumerable<string> PhoneNumbers);
public record PublisherToUpdateDto(string Name, DateOnly Established);
