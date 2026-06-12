using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Etmen_BLL.Repositories.IServices;
using System.Security.Claims;

namespace Etmen_PL.Filters
{
    public class DoctorOnboardingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user.Identity?.IsAuthenticated == true && user.IsInRole("Doctor"))
            {
                var doctorService = context.HttpContext.RequestServices.GetService<IDoctorService>();
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (doctorService != null && !string.IsNullOrEmpty(userId))
                {
                    var profileResult = await doctorService.GetProfileAsync(userId);
                    if (profileResult.IsSuccess && profileResult.Data != null && !profileResult.Data.IsOnboarded)
                    {
                        var controller = context.RouteData.Values["controller"]?.ToString();
                        var action = context.RouteData.Values["action"]?.ToString();
                        
                        // Allow Onboarding page actions and Account Logout action
                        bool isAllowedAction = (controller == "DoctorDashboard" && (action == "Onboarding" || action == "SaveOnboarding"))
                                               || (controller == "Account" && action == "Logout");
                                               
                        if (!isAllowedAction)
                        {
                            context.Result = new RedirectToActionResult("Onboarding", "DoctorDashboard", null);
                            return;
                        }
                    }
                }
            }
            await next();
        }
    }
}
