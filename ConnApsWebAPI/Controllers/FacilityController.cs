using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnApsDomain.Models;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Facility")]
    public class FacilityController : BaseController
    {
        // POST api/Facility/Create
        [HttpPost, Route("Create")]
        public IHttpActionResult CreateFacility(FaciltyRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.CreateFacility(User.Identity.GetUserId(), model.Level, model.Number);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        // GET api/Facility
        [HttpGet]
        public IHttpActionResult FetchFacilities()
        {
            IEnumerable<IFacility> facilities;
            try
            {
                facilities = Cad.FetchFacilities(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<IFacility>>(facilities);
        }

        // GET api/Facility
        [HttpGet]
        public IHttpActionResult FetchFacility(int facilityId)
        {
            IFacility facility;
            try
            {
                facility = Cad.FetchFacility(User.Identity.GetUserId(), facilityId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IFacility>(facility);
        }
    }
}
