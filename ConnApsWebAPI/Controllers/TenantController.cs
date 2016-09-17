using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "Tenant")]
    [RoutePrefix("api/Tenant")]
    public class TenantController : BaseController
    {
        protected TenantController CAD;

        public TenantController()
        {
            CAD = new TenantController();
        }

        // GET api/BuildingManager/Test
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "this works";
        }
    }
}
