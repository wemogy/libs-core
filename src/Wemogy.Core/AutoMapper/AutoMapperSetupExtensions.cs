using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wemogy.Core.AutoMapper
{
    public static class AutoMapperSetupExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Func<IServiceProvider, Profile> profileResolver)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profileResolver(provider));
            }).CreateMapper());

            return services;
        }
    }
}
