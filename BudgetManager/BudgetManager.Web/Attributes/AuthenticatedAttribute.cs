using System;
using System.Web.Mvc;
using System.Web.Routing;
using BudgetManager.Web.Models;

namespace BudgetManager.Web.Attributes
{
    public class AuthenticatedAttribute : AuthorizeAttribute
    {
        private const string SessionKey = "SessionKey{31768B33-792D-45CF-9D6D-CEABD4FCFDFF}";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var session = filterContext.HttpContext.Session[SessionKey] as Session;
                if (session == null || session.User == null || !session.User.IsAuthenticated || session.User.IsLocked)
                {
                    string returnUrl = null;
                    if (filterContext.HttpContext.Request.HttpMethod.Equals("GET",
                        StringComparison.CurrentCultureIgnoreCase))
                        returnUrl = filterContext.HttpContext.Request.Url != null
                            ? filterContext.HttpContext.Request.Url.PathAndQuery
                            : filterContext.HttpContext.Request.RawUrl;

                    // Redirect to login page
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"controller", "Account"},
                            {"action", "Login"},
                            {"ReturnUrl", returnUrl}
                        });
                }
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }
    }

    public class AuthenticatedAdminAttribute : AuthorizeAttribute
    {
        private const string SessionKey = "SessionKey{31768B33-792D-45CF-9D6D-CEABD4FCFDFF}";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var session = filterContext.HttpContext.Session[SessionKey] as Session;
                if (session == null || session.User == null || !session.User.IsAuthenticated || session.User.IsLocked ||
                    !session.User.IsAdmin)
                {
                    string returnUrl = null;
                    if (filterContext.HttpContext.Request.HttpMethod.Equals("GET",
                        StringComparison.CurrentCultureIgnoreCase))
                        returnUrl = filterContext.HttpContext.Request.Url != null
                            ? filterContext.HttpContext.Request.Url.PathAndQuery
                            : filterContext.HttpContext.Request.RawUrl;

                    // Redirect to login page
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"controller", "Account"},
                            {"action", "Login"},
                            {"ReturnUrl", returnUrl}
                        });
                }
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}