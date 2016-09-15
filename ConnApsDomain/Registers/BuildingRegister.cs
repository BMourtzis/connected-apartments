using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingRegister: IDisposable
    {
        #region Constructors

        public BuildingRegister() { }

        #endregion

        #region Properties

        ConnApsContext context = new ConnApsContext();

        #endregion

        #region Building

        public IBuilding CreateBuilding(string buildingName, string address)
        {
            Building building = new Building(buildingName, address);
            context.Buildings.Add(building);
            context.SaveChanges();
            return building;
        }

        public IBuilding UpdateBuilding(int buildingId, string buildingName, string address)
        {
            Building building = getBuilding(buildingId);
            building.UpdateBuilding(buildingName, address);
            context.SaveChanges();
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            Building bu = context.Buildings
                                 .Include("Locations")
                                 .Include("BuildingManager")
                                 .Where(b => b.Id.Equals(buildingId))
                                 .FirstOrDefault();
            return bu;
        }

        private Building getBuilding(int buildingId)
        {
            Building bu = context.Buildings
                                 .Where(b => b.Id.Equals(buildingId))
                                 .FirstOrDefault();
            return bu;
        }

        public IEnumerable<IApartment> GetBuildingApartments(int buildingId)
        {
            var building = FetchBuilding(buildingId);
            return building.Apartments;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var building = getBuilding(buildingId);
            Apartment apt = building.CreateApartment(level, number, tenantsAllowed, facingDirection);
            context.Apartments.Add(apt);
            context.SaveChanges();
            return apt;
        }

        public IApartment UpdateApartment(int apartmentId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            Apartment apt = getApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            context.SaveChanges();
            return apt;
        }

        public IApartment FetchApartment(int apartmentId)
        {
            Apartment apt = context.Apartments
                                   .Include("Tenants")
                                   .Where(a => a.Id.Equals(apartmentId))
                                   .FirstOrDefault();
            return apt;
        }

        private Apartment getApartment(int apartmentId)
        {
            Apartment apt = context.Apartments
                                   .Where(a => a.Id.Equals(apartmentId))
                                   .FirstOrDefault();
            return apt;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion
    }
}
