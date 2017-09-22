using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RequestForService.Web.Controllers.Base
{
	public class BaseController : Controller
	{
		private const string SessionKey = "66E4F2EA-112F-4213-8448-9DA582C43F79";

		protected Models.Session session { get; set; }

		public Guid? UserId
		{
			get { return session != null && session.User != null ? session.User.Id : (Guid?) null; }
		}

		protected string SiteUrl
		{
			get
			{
				return Request.Url != null ? string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority) : string.Empty;
			}
		}

		protected override void Initialize(RequestContext requestContext)
		{
			SetSession(requestContext.HttpContext);
			base.Initialize(requestContext);
			ViewBag.User = session.User;
			ViewBag.ReturnUrl = session.ReturnUrl;
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			HandleOnException(filterContext);
		}

		private void HandleOnException(ExceptionContext filterContext)
		{
			Business.Services.Errors.Logs.Log(filterContext.Exception, UserId);
			//base.OnException(filterContext);
			if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				filterContext.Result = new JsonResult
				{
				    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				    Data = new
				    {
				        error = true,
				        message = filterContext.Exception.Message
				    }
				};
			}
			else
			{
				//var controllerName = (string) filterContext.RouteData.Values["controller"];
				//var actionName = (string) filterContext.RouteData.Values["action"];
				//var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
				//filterContext.Result = new ViewResult
				//{
				//	ViewName = "Error",
				//	MasterName = "",
				//	ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
				//	TempData = filterContext.Controller.TempData
				//};
				filterContext.Result = new RedirectResult("/Error");
			}
			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			SetSession(filterContext.HttpContext);
			base.OnActionExecuted(filterContext);
		}

		private void SetSession(HttpContextBase httpContext)
		{
			if (httpContext.Session == null) return;
			session = httpContext.Session[SessionKey] as Models.Session;
			if (session == null)
				httpContext.Session[SessionKey] = session = new Models.Session();
		}

		protected void ClearSession()
		{
			//make sure to clear all session variables here.

			session.User = null;
		}








	}

}
