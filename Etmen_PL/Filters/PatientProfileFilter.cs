using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Etmen_DAL.Repositories.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Etmen_PL.Filters
{
    public class PatientProfileFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user.Identity?.IsAuthenticated == true && user.IsInRole("Patient"))
            {
                var uow = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (uow != null && !string.IsNullOrEmpty(userId))
                {
                    var profile = await uow.PatientProfiles.Table
                        .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
                        
                    if (profile != null)
                    {
                        context.HttpContext.Items["PatientProfile"] = profile;
                    }
                }
            }
            await next();
        }
    }
}
