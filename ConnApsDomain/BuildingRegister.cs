using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingRegister
    {
        #region Constructors

        public BuildingRegister() { }

        #endregion

        #region Properties



        #endregion

        #region Functions

        public IBuilding CreateBuilding(string buildingName, string address)
        {
            Building building;
            using (var context = new ConnApsContext())
            {
                building = new Building(buildingName, address);
                context.Buildings.Add(building);
                context.SaveChanges();
            }
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            Building bu;
            using (var context = new ConnApsContext())
            {
                bu = context.Buildings.Include("Locations")
                           .Include("BuildingManager")
                           .Where(b => b.Id.Equals(buildingId))
                           .FirstOrDefault();
            }
            return bu;
        }

        private Building getBuilding(int buildingId)
        {
            Building bu;
            using (var context = new ConnApsContext())
            {
                bu = context.Buildings.Find(buildingId);
            }
            return bu;
        }

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            Apartment apt;
            using (var context = new ConnApsContext())
            {
                var building = getBuilding(buildingId);
                apt = building.CreateApartment(level, number, tenantsAllowed, facingDirection);
                context.Apartments.Add(apt);
                context.SaveChanges();

            }
            return apt;
        }

        public IApartment FetchApartment(int apartmentId)
        {
            Apartment apt;
            using (var context = new ConnApsContext())
            {
                apt = context.Apartments.Find(apartmentId);
            }
            return apt;
        }

        #endregion
    }
}
