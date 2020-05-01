using System;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;

namespace ConnectCDS
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyCDSServer"].ConnectionString;
            CrmServiceClient svc = new CrmServiceClient(connectionString);
            if (svc.IsReady)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine(svc.LastCrmError);
            }
        }
    }
}
