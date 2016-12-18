using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers.API.V1
{
    /// <summary>
    /// This controller is responsible for all the functions of the Apartment class.
    /// </summary>

    [Authorize, RoutePrefix("api/v1/Apartment")]
    public class ApartmentController : BaseController
    {
        /// <summary>
        /// Fetches the Apartment of the Tenant
        /// </summary>
        /// <returns>Returns the apartment details or an Error Message</returns>

        // GET api/Apartment/
        [Authorize(Roles = "Tenant"), HttpGet, Route()]
        public IHttpActionResult FetchApartment()
        {
            IApartment apt;
            try
            {
                apt = Cad.FetchApartment(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<IApartment>(apt);
        }

        /// <summary>
        /// Fetches an apartment, with the id given.
        /// The apartment needs to be in the same building as the tenant
        /// </summary>
        /// <param name="id">The Id of the apartment</param>
        /// <returns>Returns the apartment details or an Error Message</returns>

        // GET api/Apartment?aptId=1
        [HttpGet, Route()]
        public IHttpActionResult FetchApartment(int id)
        {
            IApartment apt;
            try
            {
                apt = Cad.FetchApartment(id, User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<IApartment>(apt);
        }

        /// <summary>
        /// Fetches all the apartment of the building that the user belongs to
        /// </summary>
        /// <returns>Returnsa list of apartment details or an Error Message</returns>

        // GET api/Apartment/Building
        [HttpGet, Route("Building")]
        public IHttpActionResult FetchBuildingApartments()
        {
            IEnumerable<IApartment> apt;
            try
            {
                apt = Cad.FetchApartments(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IEnumerable<IApartment>>(apt);
        }

        /// <summary>
        /// Creates a new apartment
        /// </summary>
        /// <param name="model">Supplies the details needed to create a new apartment</param>
        /// <returns>Returns a default response or an Error Message</returns>

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
                Cad.CreateApartment(model.Level, model.Number, model.TenantsAllowed, model.FacingDirection,
                    User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return GetResponse();
        }

        /// <summary>
        /// Updates the  apartment details
        /// </summary>
        /// <param name="model">Includes all the informaiton needed to update the apartment</param>
        /// <returns>Returns a default response or an Error Message</returns>

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
                Cad.UpdateApartment(model.Id, model.Level, model.Number, model.TenantsAllowed, model.FacingDirection,
                    User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return GetResponse();
        }
    }
}
