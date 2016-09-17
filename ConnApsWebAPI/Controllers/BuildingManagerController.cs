using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "BuildingManager")]
    [RoutePrefix("api/BuildingManager")]
    public class BuildingManagerController : BaseController
    {
        protected BuildingManagerFacade CAD;

        public BuildingManagerController()
        {
            CAD = new BuildingManagerFacade();
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
