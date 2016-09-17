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

        #endregion
    }
}
