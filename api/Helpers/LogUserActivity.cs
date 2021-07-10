using System;
using System.Threading.Tasks;
using api.Extentions;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var resultContext=await next();
           if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
           //var username = resultContext.HttpContext.User.GetUsername();
           var userID = resultContext.HttpContext.User.GetUserId();
           var uow =resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
           var user= await uow.UserRepository.GetUserByIdAsync(userID);
           user.LastActive=DateTime.UtcNow;
           await uow.Complete();
        }
    }
}