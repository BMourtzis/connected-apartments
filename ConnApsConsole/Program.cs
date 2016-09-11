using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AdminFacade af = new AdminFacade();

            var bm = af.FetchBuildingManager(1);

            int i = 0;
        }
    }
}
