using System;
using ConnApsDomain.Models;

namespace ConnApsDomain.Facades
{
    public class AdminFacade: Facade
    {
        public AdminFacade() : base() { }

        #region Building

        public IBuilding CreateBuilding(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, string buildingname, string newAddress)
        {
            var building = BuildingRegister.CreateBuilding(buildingname, newAddress);
            var bm = PersonRegister.CreateBuildingManager(firstname, lastname, dateofbirth, newPhone, userid, building.Id);
            return building;
        }

        #endregion

        #region BuildingManager

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var manager = PersonRegister.FetchBuildingManager(userId);
            return manager;
        }

        #endregion

        #region Tenant

        #endregion
    }
}
