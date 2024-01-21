using GameLib.Domain.ValueObjects;

namespace GameLib.Core.Dtos;

public record PublisherToCreateDto(string Name, DateOnly? Established, List<PhoneNumberDto>? PhoneNumbers);
public record PublisherToReturnDto(int Id, string Name, DateOnly? Established, List<PhoneNumber> PhoneNumbers);
public record PublisherToUpdateDto(string Name, DateOnly Established, List<PhoneNumberDto>? PhoneNumbers);
