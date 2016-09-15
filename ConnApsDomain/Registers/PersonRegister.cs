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

        public IBuildingManager UpdateBuildingManager(int managerId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            BuildingManager bm = getBuildingManager(managerId);
            bm.UpdateBuildingManager(firstname, lastname, dateofbirth, newPhone);
            context.SaveChanges();
            return bm;
        }

        public IBuildingManager FetchBuildingManager(int managerId)
        {
            BuildingManager bm = context.BuildingManagers.Include("Building")
                                        .Where(m => m.Id.Equals(managerId))
                                        .FirstOrDefault();
            return bm;

        }

        private BuildingManager getBuildingManager(int managerId)
        {
            BuildingManager bm = context.BuildingManagers
                                        .Where(m => m.Id.Equals(managerId))
                                        .FirstOrDefault();
            return bm;
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

        public ITenant UpdateTenant(int tenantId, string firstName, string lastName, DateTime dob, string phone, int apartmentId)
        {
            Tenant tenant = getTenant(tenantId);
            tenant.UpdateTenant(firstName, lastName, dob, phone, apartmentId);
            context.SaveChanges();
            return tenant;
        }

        public ITenant FetchTenant(int tenantId)
        {
            Tenant tenant = context.Tenants.Include("Apartment")
                                   .Where(t => t.Id.Equals(tenantId))
                                   .FirstOrDefault();
            return tenant;
        }

        private Tenant getTenant(int tenantId)
        {
            Tenant tenant = context.Tenants
                                   .Where(t => t.Id.Equals(tenantId))
                                   .FirstOrDefault();
            return tenant;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion
    }
}
