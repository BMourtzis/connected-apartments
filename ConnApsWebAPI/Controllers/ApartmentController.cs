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
    [Authorize, RoutePrefix("api/Apartment")]
    public class ApartmentController : BaseController
    {
        /// <summary>
        /// Returns a List of Apartments that belong to the building
        /// </summary>
        /// <returns>A Response that includes a list of apartments</returns>

        // GET api/Apartment
        [HttpGet]
        public IHttpActionResult FetchApartment()
        {
            IEnumerable<IApartment> apt;
            try
            {
                apt = Cad.FetchApartments(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok<IEnumerable<IApartment>>(apt);
        }

        // GET api/Apartment
        [HttpGet]
        public IHttpActionResult FetchApartment(int aptId)
        {
            IApartment apt;
            try
            {
                apt = Cad.FetchApartment(aptId, User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IApartment>(apt);
        }

        // POST api/Apartment/Create
        [Authorize(Roles = "BuildingManager"), HttpPost, Route("Create")]
        public IHttpActionResult CreateApartment(ApartmentBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        // POST api/Apartment/Update
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("UpdateApartment")]
        public IHttpActionResult UpdateApartment(ApartmentUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateApartment(model.Id, model.Level, model.Number, model.TenantsAllowed, model.FacingDirection, User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return GetResponse();
        }
    }
}
