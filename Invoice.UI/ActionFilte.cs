using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Invoice.UI
{
    public class ActionFilteToken : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("token")))
            {
                context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Result = new RedirectResult("Account/LogIn");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
