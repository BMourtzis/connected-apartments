using System;
using ConnApsDomain.Registers;

namespace ConnApsDomain.Facades
{
    public class Facade: IDisposable, IDisposableFacade
    {
        internal readonly BuildingRegister BuildingRegister;
        internal readonly PersonRegister PersonRegister;

        public Facade()
        {
            BuildingRegister = new BuildingRegister();
            PersonRegister = new PersonRegister();
        }



        public void Dispose()
        {
            BuildingRegister.Dispose();
            PersonRegister.Dispose();
        }
    }
}
