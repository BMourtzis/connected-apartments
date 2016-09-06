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

        public IBuilding AddBuilding(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, string buildingname, string newAddress)
        {
            var bm = personRegister.addBuildingManager(firstname, lastname, dateofbirth, newPhone, userid);
            var building = buildingRegister.addBuilding(buildingname, newAddress, bm.BuildingManagerId);
            return building;
        }

        #endregion

        #region BuildingManager

        #endregion
    }
}
