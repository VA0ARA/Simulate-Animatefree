using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace AFAP.EndPointMVCPattern.Infrastructure
{

    public class CustomAuthorizeAttributeAccess : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // هدایت کاربر به صفحه لاگین
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" })
                );
            }
            else
            {

                var hasAccess = context.HttpContext.User.IsInRole("Owner");

                if (!hasAccess)
                {
                    // هدایت کاربر به صفحه Unauthorized
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "Accesslimitation" })
                    );
                }
            }
        }
    }

}
