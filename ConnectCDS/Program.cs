using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace ConnectCDS
{
	class Program
	{
		static void Main(string[] args)
		{
			CrmServiceClient svc = OrgServiceProvider.Instance.OrgService;
			if (svc.IsReady)
			{
				var accountQuery = new QueryExpression("account");
				accountQuery.ColumnSet = new ColumnSet(new[] { "address1_line1" });
				var list = svc.RetrieveMultiple(accountQuery);
				list.Entities.ToList().ForEach(a => Console.WriteLine(a.Attributes.Contains("address1_line1") ? a.Attributes["address1_line1"] :
					"\"\""));

				Guid accountId = (Guid)list.Entities.FirstOrDefault()["accountid"];
				ContactsByAccountId(accountId, svc);
			}
			else
			{
				Console.WriteLine(svc.LastCrmError);
			}
		}

		private static void ContactsByAccountId(Guid accountId, CrmServiceClient svc)
		{
			var context = new OrganizationServiceContext(svc);
			var query = from c in context.CreateQuery("contact")
						join a in context.CreateQuery("account") on
							c["parentcustomerid"] equals a["accountid"]
						where (Guid)a["accountid"] == accountId
						select new
						{
							FirstName = c["firstname"] ?? "",
							LastName = c["lastname"] ?? ""
						};
			foreach (var c in query)
			{
				Console.WriteLine($"First name: {c.FirstName}, Last name: {c.LastName}");
			}
		}
	}

}
