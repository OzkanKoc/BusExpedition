using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageFramework;

namespace VoyageApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Route route = new Route("istanbul", "çanakkale", 500);
            BusExpedition bExpedition = new BusExpedition(route, new DateTime(2019, 5, 15));
            bExpedition.AddDriver(new Driver("özkan", "koç", LicenseType.HighLicense, new DateTime(1993, 5, 15)));
        }
    }
}
