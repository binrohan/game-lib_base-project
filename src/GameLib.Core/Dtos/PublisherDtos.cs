using GameLib.Domain.ValueObjects;

namespace GameLib.Core;

public record PublisherToCreateDto(string Name, DateOnly? Established);
public record PublisherToListDto(int Id, string Name, DateOnly? Established, IEnumerable<string> PhoneNumbers);
public record PublisherToUpdateDto(string Name, DateOnly Established);
