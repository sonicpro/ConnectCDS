using System;
using System.Configuration;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;

namespace ConnectCDS
{
	public sealed class OrgServiceProvider : IDisposable
	{
		private static OrgServiceProvider instance = new OrgServiceProvider();
		private static CrmServiceClient svc;

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static OrgServiceProvider()
		{
		}

		private OrgServiceProvider()
		{
			var settings = ConfigurationManager.AppSettings;
			var cred = new NetworkCredential(settings["UserName"], settings["Password"]);
			svc = new CrmServiceClient(cred, AuthenticationType.AD, settings["HostName"], settings["Port"], settings["OrgName"]);
		}

		public CrmServiceClient OrgService => svc;

		public static OrgServiceProvider Instance => instance;

		public void Dispose()
		{
			svc.Dispose();
		}
	}
}
