﻿using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Repositories;
using GameLib.Core.Interfaces.Services;
using GameLib.Core.Serivces;
using GameLib.Infrastructure;
using GameLib.Infrastructure.Data.Repositories;

namespace GameLib.API.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IGameService, GameService>();

        return services;
    }
}
