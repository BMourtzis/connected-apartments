using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public abstract class Facade: IDisposable, IDisposableFacade
    {
        internal BuildingRegister buildingRegister;
        internal PersonRegister personRegister;

        public Facade()
        {
            buildingRegister = new BuildingRegister();
            personRegister = new PersonRegister();
        }

        public void Dispose()
        {
            buildingRegister.Dispose();
            personRegister.Dispose();
        }
    }
}
