using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class TenantFacade: Facade
    {
        #region Tenant

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

        #endregion
    }
}
