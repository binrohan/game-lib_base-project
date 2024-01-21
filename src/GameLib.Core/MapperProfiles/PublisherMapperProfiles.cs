using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Domain.Entities;
using GameLib.Domain.ValueObjects;

namespace GameLib.Core;

public class PublisherMapperProfiles : Profile
{
    public PublisherMapperProfiles()
    {
        CreateMap<PublisherToCreateDto, Publisher>()
        .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => MapPhoneNumberDtosToPhoneNumbers(src.PhoneNumbers)));
        CreateMap<PublisherToUpdateDto, Publisher>();
        CreateMap<Publisher, PublisherToListDto>();
    }

    
    private static List<PhoneNumber> MapPhoneNumberDtosToPhoneNumbers(IEnumerable<PhoneNumberDto>? phoneNumbers)
    {
        List<PhoneNumber> result = [];

        if (phoneNumbers != null)
        {
            foreach (var phoneNumberDto in phoneNumbers)
            {
                PhoneNumber phoneNumber = new(
                    number: phoneNumberDto.Number,
                    countryCode: phoneNumberDto.CountryCode,
                    extension: phoneNumberDto.Extension
                );

                result.Add(phoneNumber);
            }
        }

        return result;
    }
}
