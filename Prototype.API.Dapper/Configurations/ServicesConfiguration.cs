﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prototype.API.DataDapper.Repositories;
using Prototype.API.Domain.Supervisors;

namespace Prototype.API.Dapper.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAlbumRepository, AlbumRepository>()
                    .AddScoped<IArtistRepository, ArtistRepository>();
            //    .AddScoped<ICustomerRepository, CustomerRepository>()
            //    .AddScoped<IEmployeeRepository, EmployeeRepository>()
            //    .AddScoped<IGenreRepository, GenreRepository>()
            //    .AddScoped<IInvoiceRepository, InvoiceRepository>()
            //    .AddScoped<IInvoiceLineRepository, InvoiceLineRepository>()
            //    .AddScoped<IMediaTypeRepository, MediaTypeRepository>()
            //    .AddScoped<IPlaylistRepository, PlaylistRepository>()
            //    .AddScoped<ITrackRepository, TrackRepository>();

            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<ISupervisor, Supervisor>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            }
        );

        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );

            return services;
        }
    }
}
