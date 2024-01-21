using GameLib.Core.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace GameLib.Core.Exceptions;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(GenreMapperProfile));
        services.AddAutoMapper(typeof(PublisherMapperProfiles));

        return services;
    }
}
