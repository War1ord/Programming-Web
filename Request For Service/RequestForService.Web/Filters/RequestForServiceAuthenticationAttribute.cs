using System.Web.Mvc;
using System.Web.Routing;

namespace RequestForService.Web.Filters
{
	public class RequestForServiceAuthenticationAttribute : AuthorizeAttribute
	{
		private const string SessionKey = "66E4F2EA-112F-4213-8448-9DA582C43F79";

		/// <summary>
		/// Called when a process requests authorization.
		/// </summary>
		/// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.Session != null)
			{
				var session = filterContext.HttpContext.Session[SessionKey] as Models.Session;
				if (session == null || session.User == null)
				{
					string returnUrl = null;
					if (filterContext.HttpContext.Request.HttpMethod.Equals("GET", System.StringComparison.CurrentCultureIgnoreCase))
						returnUrl = filterContext.HttpContext.Request.RawUrl;

					filterContext.Result = LoginResult(returnUrl);
				}
			}
		}

		/// <summary>
		/// Processes HTTP requests that fail authorization.
		/// </summary>
		/// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>. The <paramref name="filterContext"/> object contains the controller, HTTP context, request context, action result, and route data.</param>
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = LoginResult(filterContext.HttpContext.Request.RawUrl);
		}

		/// <summary>
		/// results a Login result.
		/// </summary>
		/// <param name="returnurl">The returnurl.</param>
		/// <returns></returns>
		private static RedirectToRouteResult LoginResult(string returnurl)
		{
			return new RedirectToRouteResult(new RouteValueDictionary
			{
				{"controller", "Account"},
				{"action", "Login"},
				{"returnurl", returnurl},
			});
		}
	}
}