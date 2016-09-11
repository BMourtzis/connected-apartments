using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class PersonRegister
    {


        #region Constructors

        public PersonRegister() { }

        #endregion

        #region Properties



        #endregion

        #region Functions

        public IBuildingManager CreateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingId)
        {
            BuildingManager bm;
            using (var context = new ConnApsContext())
            {
                bm = new BuildingManager(firstname, lastname, dateofbirth, newPhone, userid, buildingId);
                context.BuildingManagers.Add(bm);
                context.SaveChanges();
            }
            return bm;
        }

        public IBuildingManager FetchBuildingManager(int managerId)
        {
            BuildingManager bm;
            using (var context = new ConnApsContext())
            {
                bm = context.BuildingManagers.Include("Building")
                            .Where(m => m.Id.Equals(managerId))
                            .FirstOrDefault();
            }
            return bm;

        }

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            Tenant tenant;
            using (var context = new ConnApsContext())
            {
                tenant = new Tenant(firstName, lastName, dob, phone, userId, apartmentId);
                context.Tenants.Add(tenant);
                context.SaveChanges();
            }
            return tenant;
        }

        public ITenant FetchTenant(int tenantId)
        {
            Tenant tenant;
            using (var context = new ConnApsContext())
            {
                tenant = context.Tenants.Include("Apartment")
                                .Where(t => t.Id.Equals(tenantId))
                                .FirstOrDefault();
            }
            return tenant;
        }

        #endregion
    }
}
