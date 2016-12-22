using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers.API.V1
{
    /// <summary>
    /// This contrller is responsible for all the functions of the Facility Class
    /// </summary>
    [Authorize, RoutePrefix("api/v1/Facility")]
    public class FacilityController : BaseController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FacilityController(): base (){ }

        /// <summary>
        /// Constructor that allowes for Dependency Injection
        /// </summary>
        /// <param name="facade"></param>
        public FacilityController(IFacade facade): base(facade) { }

        /// <summary>
        /// Fetches all the faciities of a building
        /// </summary>
        /// <returns>Returns a list of the facility details or an Error Message</returns>

        // GET api/Facility
        [HttpGet, Route()]
        public IHttpActionResult FetchFacilities()
        {
            IEnumerable<IFacility> facilities;
            try
            {
                facilities = Cad.FetchFacilities(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IEnumerable<IFacility>>(facilities);
        }

        /// <summary>
        /// Fetches a facility
        /// </summary>
        /// <param name="id">The Id of the facility</param>
        /// <returns>Returns the facility details or an Error Message</returns>

        // GET api/Facility
        [HttpGet, Route()]
        public IHttpActionResult FetchFacility(int id)
        {
            IFacility facility;
            try
            {
                facility = Cad.FetchFacility(User.Identity.GetUserId(), id);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IFacility>(facility);
        }

        /// <summary>
        /// Creates a new Facility
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

        // POST api/Facility/Create
        [Authorize(Roles = "BuildingManager"), HttpPost, Route("Create")]
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
        /// Updates the Facility details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an Error Message</returns>

        //PUT api/Facility/Update
        [Authorize(Roles = "BuildingManager"), HttpPut, Route("Update")]
        public IHttpActionResult UpdateFacility(FaciltyUpdateModel model)
        {
            try
            {
                Cad.UpdateFacility(User.Identity.GetUserId(), model.Id, model.Level, model.Number);
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
