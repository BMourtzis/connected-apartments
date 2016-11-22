using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnApsDomain.Models;
using Microsoft.AspNet.Identity;
using ConnApsWebAPI.Models;

namespace ConnApsWebAPI.Controllers
{
    [Authorize(Roles = "Tenant")]
    [RoutePrefix("api/Tenant")]
    public class TenantController : BaseController
    {

        public TenantController()
        {
            Cad = new Facade();
        }

        public TenantController(Facade facade)
        {
            Cad = facade;
        }

        #region Tenant

        // GET api/BuildingManager/TenantInfo
        [HttpGet]
        [Route("TenantInfo")]
        public IHttpActionResult FetchTenant()
        {
            ITenant t;
            try
            {
                t = Cad.FetchTenant(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<ITenant>(t);
        }

        // PUT api/BuildingManager/UpdateTenant
        [HttpPut]
        [Route("UpdateTenant")]
        public IHttpActionResult UpdateTenant(TenantUpdateModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ITenant t;
            try
            {
                t = Cad.UpdateTenant(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return GetResponse();
        }

        #endregion

        #region Facility

        [HttpGet]
        [Route("FetchFacilities")]
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

        #endregion

        #region Booking

        [HttpPost]
        [Route("CreateBooking")]
        public IHttpActionResult CreateBooking(BookingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Cad.CreateBooking(User.Identity.GetUserId(), model.FacilityId, model.StartTime, model.EndTime);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return GetResponse();
        }

        [HttpGet]
        [Route("FetchBooking")]
        public IHttpActionResult FetchBooking(int facilityId, int bookingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IBooking booking;
            try
            {
                booking = Cad.FetchBooking(User.Identity.GetUserId(), facilityId, bookingId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IBooking>(booking);
        }

        [HttpGet]
        [Route("FetchFacilityBookings")]
        public IHttpActionResult FetchFacilityBookings(int facilityId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId(), facilityId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok<IEnumerable<IBooking>>(bookings);
        }

        [HttpGet]
        [Route("FetchPersonBookings")]
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

        [HttpDelete]
        [Route("CancelBooking")]
        public IHttpActionResult CancelBooking(BookingCancelModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

    }
}
