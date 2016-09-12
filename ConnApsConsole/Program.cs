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
            var bmf = new BuildingManagerFacade();

            var tenant = bmf.UpdateTenant(2, "Michael", "Condon", new DateTime(1994, 6, 9), "0123456789", 1);

            int i = 0;
        }
    }
}
