using System.Web.Optimization;

namespace RequestForService.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr")
				.Include("~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
				.Include("~/Scripts/bootstrap.js",
						 "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/requestforservice")
				.Include("~/Scripts/requestforservice-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/workordercreate")
				.Include("~/Scripts/workordercreate-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/workorderdetails")
				.Include("~/Scripts/workorderdetails-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/accountupdate")
				.Include("~/Scripts/accountupdate-{version}.js"));

			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/Content/bootstrap.css",
						 "~/Content/site.css",
						 "~/Content/custom.css",
						 "~/Content/theme.css"));

		}
	}
}
