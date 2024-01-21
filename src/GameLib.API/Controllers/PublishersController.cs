using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Domain.Entities;

namespace GameLib.API.Controllers;

public class PublishersController(IPublisherService service)
    : CrudOperationController<Publisher, PublisherToCreateDto, PublisherToListDto, PublisherToUpdateDto>(service)
{
    private readonly IPublisherService _service = service;
}
