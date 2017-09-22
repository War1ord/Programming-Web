using System.Web;

namespace BudgetManager.Common
{
	/// <summary>
	/// Environment object
	/// </summary>
	public static class Environment
	{
		/// <summary>
		/// Environment name (host name)
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static string HostName(HttpContext context = null)
		{
			string hostName = string.Empty;
			try
			{
				if (context != null)
				{
					hostName = context.Server.MachineName;
				}
				else
				{
					hostName = System.Environment.MachineName;
				}
			}
			catch
			{
				/* ignore */
			}
			return hostName;
		}

		/// <summary>
		/// Environment version (host version)
		/// </summary>
		/// <returns></returns>
		public static string HostVersion()
		{
			string version = string.Empty;
			try
			{
				version = System.Environment.Version.ToString();
			}
			catch
			{
				/* ignore */
			}
			return version;
		}
	}
}