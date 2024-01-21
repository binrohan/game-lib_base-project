using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Domain.Entities;

namespace GameLib.Core.Serivces;

public class PublisherService(IUnitOfWork unitOfWork, IMapper mapper) 
    : CrudOpserationService<Publisher, PublisherToCreateDto, PublisherToListDto, PublisherToUpdateDto>(unitOfWork, mapper),
      IPublisherService
{

}
