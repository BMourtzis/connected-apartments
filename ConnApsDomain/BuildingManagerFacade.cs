using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class BuildingManagerFacade: Facade
    {

        #region Building

        public IBuilding UpdateBuilding(int buildingId, string buildingName, string address)
        {
            var building = buildingRegister.UpdateBuilding(buildingId, buildingName, address);
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = buildingRegister.FetchBuilding(buildingId);
            return building;
        }

        public IBuilding GetTenantBuilding(int tenantId)
        {

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

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            var tenant = personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId);
            return tenant;
        }

        public ITenant FetchTenant(int tenantId)
        {
            var tenant = personRegister.FetchTenant(tenantId);
            return tenant;
        }

        public ITenant UpdateTenant(int tenantId, string firstName, string lastName, DateTime dob, string phone, int apartmentId)
        {
            var tenant = personRegister.UpdateTenant(tenantId, firstName, lastName, dob, phone, apartmentId);
            return tenant;
        }

        #endregion

    }
}
