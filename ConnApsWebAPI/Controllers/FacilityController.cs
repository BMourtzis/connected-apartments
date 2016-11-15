using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConnApsWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Facility")]
    public class FacilityController : BaseController
    {
    }
}
