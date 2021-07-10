using api.Data;
using api.Helpers;
using api.Interfaces;
using api.services;
using api.SignalR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddSingleton<PresenceTracker>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IPhotoService,PhotoService>();
            // services.AddScoped<IUserRepository,UserRepository>();
            // services.AddScoped<ILikesRepository,LikesRepository>();
            // services.AddScoped<IMessageRepository,MessageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>{         
            
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));            

            } );
            return services;
        }

    }
}