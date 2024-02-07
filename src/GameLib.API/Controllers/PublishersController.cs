using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.API.Controllers;

public class PublishersController(ICrudOperationService<Publisher, PublisherToCreateDto, PublisherToReturnDto, PublisherToUpdateDto> service)
    : CrudOperationController<Publisher, PublisherToCreateDto, PublisherToReturnDto, PublisherToUpdateDto>(service)
{
    
}
