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

            var b = af.AddBuilding("Bill", "Mourtzis", new DateTime(1995, 3, 16), "0420828985", "134567", "M Apartments", "32 Francis St");

            int i = 0;
        }
    }
}
