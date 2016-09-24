using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class BuildingManagerFacade: Facade
    {

        #region BuildingManager

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var bm = personRegister.FetchBuildingManager(userId);
            return bm;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var bm = personRegister.UpdateBuildingManager(userId, firstname, lastname, dateofbirth, newPhone);
            return bm;
        }

        #endregion

        #region Building

        public IBuilding UpdateBuilding(string userId, string buildingName, string address)
        {
            var building = buildingRegister.UpdateBuilding(personRegister.FetchBuildingManagerBuildingId(userId), buildingName, address);
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = buildingRegister.FetchBuilding(buildingId);
            return building;
        }

        public IBuilding GetTenantBuilding(string userId)
        {
            var building = buildingRegister.FetchBuilding(personRegister.FetchTenant(userId).BuildingId);
            return building;
        }

        public IBuilding FetchBuildingManagerBuilding(string userId)
        {
            var building = buildingRegister.FetchBuilding(personRegister.FetchBuildingManagerBuildingId(userId));
            return building;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var apt = buildingRegister.CreateApartment(level, number, tenantsAllowed, facingDirection, buildingId);
            return apt;
        }

        public IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            var apt = buildingRegister.UpdateApartment(aptId, level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        public IApartment FetchApartment(int apartmentId)
        {
            var apt = buildingRegister.FetchApartment(apartmentId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(string userId)
        {
            var apt = buildingRegister.FetchApartments(personRegister.FetchBuildingManagerBuildingId(userId));
            return apt;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            var tenant = personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId);
            return tenant;
        }

        public ITenant FetchTenant(string userId)
        {
            var tenant = personRegister.FetchTenant(userId);
            return tenant;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = personRegister.UpdateTenant(userId, firstName, lastName, dob, phone);
            return tenant;
        }

        public ITenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = personRegister.ChangeApartment(userId, apartmentId);
            return tenant;
        }

        public IEnumerable<ITenant> FetchBuildingTenants(string userId)
        {
            var tenants = personRegister.FetchBuildingTenants(personRegister.FetchBuildingManagerBuildingId(userId));
            return tenants;
        }

        #endregion

    }
}
