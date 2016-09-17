using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "Tenant")]
    [RoutePrefix("api/Tenant")]
    public class TenantController : BaseController
    {
        protected TenantFacade CAD;

        public TenantController()
        {
            CAD = new TenantFacade();
        }

        #region Tenant

        // GET api/BuildingManager/TenantInfo
        [HttpGet]
        [Route("TenantInfo")]
        public ITenant FetchTenant()
        {
            var t = CAD.FetchTenant(User.Identity.GetUserId());
            return t;
        }

        #endregion

    }
}
