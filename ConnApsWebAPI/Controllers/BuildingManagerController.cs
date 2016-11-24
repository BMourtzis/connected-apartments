using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain.Models;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Manager")]
    public class BuildingManagerController : BaseController
    {
        // GET api/BuildingManager
        [HttpGet]
        public IHttpActionResult FetchBuildingManagerInfo()
        {
            IBuildingManager bm;
            try
            {
                bm = Cad.FetchBuildingManager(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IBuildingManager>(bm);
        }

        // Post api/BuildingManager/Update
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("Update")]
        public IHttpActionResult UpdateBuildingManager(BuildingManagerBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateBuildingManager(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

    }
}
