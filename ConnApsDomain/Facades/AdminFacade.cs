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

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var manager = personRegister.FetchBuildingManager(userId);
            return manager;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            var tenant = personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId);
            return tenant;
        }

        #endregion
    }
}
