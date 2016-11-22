using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain.Models;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "BuildingManager"), RoutePrefix("api/Manager")]
    public class BuildingManagerController : BaseController
    {
        public BuildingManagerController(): base() {}

        public BuildingManagerController(Facade facade): base(facade) { }

        #region Building Manager

        // GET api/BuildingManager/BuildingManagerInfo
        [HttpGet, Route("BuildingManagerInfo")]
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

        // Post api/BuildingManager/UpdateBuildingManager
        [HttpPut, Route("UpdateBuildingManager")]
        public IHttpActionResult UpdateBuildingManager(BuildingManagerBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IBuildingManager bm;
            try
            {
                bm = Cad.UpdateBuildingManager(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        #endregion

        #region Building

        // GET api/BuildingManager/BuildingInfo
        [HttpGet, Route("BuildingInfo")]
        public IHttpActionResult FetchBuildingInfo()
        {
            IBuilding building;
            try
            {
                building = Cad.FetchBuilding(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IBuilding>(building);
        }

        // Post api/BuildingManager/UpdateBuilding
        [HttpPut, Route("UpdateBuilding")]
        public IHttpActionResult UpdateBuilding(BuildingBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateBuilding(User.Identity.GetUserId(), model.BuildingName, model.Address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        #endregion

        #region Apartment

        /// <summary>
        /// Returns a List of Apartments that belong to the building
        /// </summary>
        /// <returns>A Response that includes a list of apartments</returns>

        // GET api/BuildingManager/FetchApartments
        [HttpGet, Route("FetchApartments")]
        public IHttpActionResult FetchApartments()
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

        // GET api/BuildingManager/FetchApartment
        [HttpGet, Route("FetchApartment")]
        public IHttpActionResult FetchApartment(int aptId)
        {
            IApartment apt;
            try
            {
                apt = Cad.FetchApartment(aptId, User.Identity.GetUserId());
            }
            catch (Exception e )
            {
                return BadRequest(e.Message);
            }
            return Ok<IApartment>(apt);
        }

        // POST api/BuildingManager/CreateApartment
        [HttpPost, Route("CreateApartment")]
        public IHttpActionResult CreateApartment(ApartmentBindingModel model)
        {
            if(!ModelState.IsValid)
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

        // POST api/BuildingManager/UpdateApartment
        [HttpPut, Route("UpdateApartment")]
        public IHttpActionResult UpdateApartment(ApartmentUpdateModel model)
        {
            if(!ModelState.IsValid)
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

        #endregion

        #region Facility

        [HttpPost, Route("CreateFacility")]
        public IHttpActionResult CreateFacility(FaciltyRegisterModel model)
        {
            if(!ModelState.IsValid)
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

        [HttpGet, Route("FetchFacilities")]
        public IHttpActionResult FetchFacilities()
        {
            IEnumerable<IFacility> facilities;
            try
            {
                facilities = Cad.FetchFacilities(User.Identity.GetUserId());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<IFacility>>(facilities);
        }

        [HttpGet, Route("FetchFacility")]
        public IHttpActionResult FetchFacility(int facilityId)
        {
            IFacility facility;
            try
            {
                facility = Cad.FetchFacility(User.Identity.GetUserId(), facilityId);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IFacility>(facility);
        }

        #endregion

        #region Booking

        [HttpPost, Route("CreateBooking")]
        public IHttpActionResult CreateBooking(BookingCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.CreateBooking(User.Identity.GetUserId(), model.FacilityId, model.StartTime, model.EndTime);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        [HttpGet,Route("FetchBooking")]
        public IHttpActionResult FetchBooking(int facilityId, int bookingId)
        {
            IBooking booking;
            try
            {
                booking = Cad.FetchBooking(User.Identity.GetUserId(), facilityId, bookingId);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IBooking>(booking);
        }

        [HttpGet, Route("FetchFacilityBookings")]
        public IHttpActionResult FetchFacilityBookings(int facilityId)
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId(), facilityId);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<IBooking>>(bookings);
        }

        [HttpGet, Route("FetchPersonBookings")]
        public IHttpActionResult FetchPersonBookings()
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<IBooking>>(bookings);
        }

        [HttpDelete, Route("CancelBooking")]
        public IHttpActionResult CancelBooking(BookingCancelModel model)
        {
            try
            {
                Cad.CancelBooking(User.Identity.GetUserId(), model.FacilityId, model.BookingId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        #endregion

        #region Tenant

        // GET api/BuildingManager/FetchTenant
        [HttpGet, Route("FetchTenant")]
        public IHttpActionResult FetchTenant(string userId)
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<ITenant>(t);
        }

        // PUT api/BuildingManager/UpdateTenant
        [HttpPut, Route("UpdateTenant")]
        public IHttpActionResult UpdateTenant(BMTenantUpdateModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.UpdateTenant(model.UserId, model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        // PUT api/BuildingManager/ChangeApartment
        [HttpPut, Route("ChangeApartment")]
        public IHttpActionResult ChangeApartment(ChangeApartmentModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.ChangeApartment(model.UserId, model.ApartmentId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        // GET api/BuildingManager/FetchBuildingTenants
        [HttpGet, Route("FetchBuildingTenants")]
        public IHttpActionResult FetchBuildingTenants()
        {
            IEnumerable<ITenant> t;
            try
            {
                t = Cad.FetchTenants(User.Identity.GetUserId());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<ITenant>>(t);
        }

        #endregion
    }
}
