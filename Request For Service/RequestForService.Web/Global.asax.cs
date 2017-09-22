using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RequestForService.Business;
using RequestForService.Business.Data;

namespace RequestForService.Web
{
	public class WebApplication : HttpApplication
	{
		private const string SessionKey = "66E4F2EA-112F-4213-8448-9DA582C43F79";

		protected WebApplication()
		{
			Error += OnError;
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataContext.Initialize();
        }

		private void OnError(object sender, EventArgs eventArgs)
		{
			Exception exception = Server.GetLastError();

            if (HttpContext.Current.Session != null)
            {
                var session = HttpContext.Current.Session[SessionKey] as Models.Session;
                if (session == null) HttpContext.Current.Session[SessionKey] = new Models.Session();
                var userId = session != null && session.User != null ? session.User.Id : (Guid?)null;

                Business.Services.Errors.Logs.Log(exception, userId);

                Context.ClearError();
                Response.Redirect("/Error"); 
            }
		}

	}
}
