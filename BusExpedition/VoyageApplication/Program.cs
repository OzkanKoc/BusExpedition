using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageFramework;
using VoyageFramework.Collection;

namespace VoyageApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Route route = new Route("istanbul", "çanakkale", 500);
            BusExpedition bExpedition = new BusExpedition(route, new DateTime(2019, 5, 15));
            bExpedition.AddDriver(new Driver("özkan", "koç", LicenseType.HighLicense, new DateTime(1993, 5, 15)));
            ListCollection<string> myList = new ListCollection<string>();
            myList.Add("merhaba");
            myList.Add("özkan");
            myList.Add("nasılsın");
            var builder = new StringBuilder(); ;
            foreach (var item in myList)
            {
                builder.Append($"{item} ");
            }
            var strArr = new string[] { "erdem", "merve", "cevat" };
            myList.CopyTo(strArr);

            var myIntList = new ListCollection<int>() { 1, 2, 3 };
            var intArr = new int[3];
            myIntList.CopyTo(intArr);
        }
    }
}
