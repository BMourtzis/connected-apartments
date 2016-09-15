using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class AdminFacade: Facade
    {
        public AdminFacade() : base() { }

        #region Building

        public IBuilding CreateBuilding(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, string buildingname, string newAddress)
        {
            var building = buildingRegister.CreateBuilding(buildingname, newAddress);
            var bm = personRegister.CreateBuildingManager(firstname, lastname, dateofbirth, newPhone, userid, building.Id);
            return building;
        }

        #endregion

        #region BuildingManager

        public IBuildingManager FetchBuildingManager(int managerId)
        {
            var manager = personRegister.FetchBuildingManager(managerId);
            return manager;
        }

        #endregion
    }
}
