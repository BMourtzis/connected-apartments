using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain.Models;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Apartment")]
    public class ApartmentController : BaseController
    {

        // GET api/Apartment/
        [Authorize(Roles = "Tenant"), HttpGet, Route()]
        public IHttpActionResult FetchApartment()
        {
            IApartment apt;
            try
            {
                apt = Cad.FetchApartment(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IApartment>(apt);
        }

        // GET api/Apartment?aptId=1
        [HttpGet, Route()]
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

        /// <summary>
        /// Returns a List of Apartments that belong to the building
        /// </summary>
        /// <returns>A Response that includes a list of apartments</returns>

        // GET api/Apartment/Building
        [HttpGet, Route("Building")]
        public IHttpActionResult FetchBuildingApartments()
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
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("Update")]
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
