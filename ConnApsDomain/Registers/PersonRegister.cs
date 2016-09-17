using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class PersonRegister: IDisposable
    {


        #region Constructors

        public PersonRegister() { }

        #endregion

        #region Properties

        ConnApsContext context = new ConnApsContext();

        #endregion

        #region BuildingManager

        public IBuildingManager CreateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingId)
        {
            BuildingManager bm = new BuildingManager(firstname, lastname, dateofbirth, newPhone, userid, buildingId);
            context.BuildingManagers.Add(bm);
            context.SaveChanges();
            return bm;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            BuildingManager bm = getBuildingManager(userId);
            bm.UpdateBuildingManager(firstname, lastname, dateofbirth, newPhone);
            context.SaveChanges();
            return bm;
        }

        public IBuildingManager FetchBuildingManager(string userId)
        {
            BuildingManager bm = context.BuildingManagers.Include("Building")
                                        .Where(m => m.UserId.Equals(userId))
                                        .FirstOrDefault();
            return bm;

        }

        private BuildingManager getBuildingManager(string userId)
        {
            BuildingManager bm = context.BuildingManagers
                                        .Where(m => m.UserId.Equals(userId))
                                        .FirstOrDefault();
            return bm;
        }

        public int FetchBuildingManagerBuildingId(string userId)
        {
            var bm = getBuildingManager(userId);
            return bm.BuildingId;
        }

        public IBuilding FetchBuildingManagerBuilding(string userId)
        {
            var bm = FetchBuildingManager(userId);
            return bm.Building;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            Tenant tenant = new Tenant(firstName, lastName, dob, phone, userId, apartmentId);
            context.Tenants.Add(tenant);
            context.SaveChanges();
            return tenant;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            Tenant tenant = getTenant(userId);
            tenant.UpdateTenant(firstName, lastName, dob, phone);
            context.SaveChanges();
            return tenant;
        }

        public ITenant FetchTenant(string userId)
        {
            Tenant tenant = context.Tenants.Include("Apartment")
                                   .Where(t => t.UserId.Equals(userId))
                                   .FirstOrDefault();
            return tenant;
        }

        public IEnumerable<ITenant> FetchTenants()
        {
            var tenants = context.Tenants.Include("Apartments");
            return tenants;
        }

        private Tenant getTenant(string userId)
        {
            Tenant tenant = context.Tenants
                                   .Where(t => t.UserId.Equals(userId))
                                   .FirstOrDefault();
            return tenant;
        }

        public ITenant ChangeApartment(string userId, int ApartmentId)
        {
            var tenant = getTenant(userId);
            tenant.ChangeApartment(ApartmentId);
            context.SaveChanges();
            return tenant;
        }

        public IEnumerable<ITenant> FetchBuildingTenants(int buildingId)
        {
            var apartments = context.Apartments.Include("Tenants").Where(t => t.BuildingId.Equals(buildingId));
            var tenants = new List<ITenant>();

            foreach(var apt in apartments)
            {
                tenants.AddRange(apt.Tenants);
            }

            return tenants;
        }

        #endregion

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
