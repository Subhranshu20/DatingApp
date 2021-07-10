using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace api.Extentions
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config)
        {

            services.AddIdentityCore<ApiUser>( opt=> {
                opt.Password.RequireNonAlphanumeric=false;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<ApiUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( option =>
                { 
                    option.TokenValidationParameters=new TokenValidationParameters{
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer=false,
                        ValidateAudience=false
                    };
                    option.Events= new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/presence"))
                            {
                                context.Token=accessToken;                                
                            }
                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddAuthorization( opt => {
                opt.AddPolicy("RequireAdminRole",policy => policy.RequireRole("Admin"));
                opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin","Moderator"));
            });    
                return services;
        }
    }
}