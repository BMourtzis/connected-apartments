using ConnApsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            CAD = new TenantFacade();
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
                using (var facade = (CAD as TenantFacade))
                {
                    t = facade.FetchTenant(User.Identity.GetUserId());
                }
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
                using (var facade = (CAD as TenantFacade))
                {
                    t = facade.UpdateTenant(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return getResponse();
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
                using (var facade = (CAD as TenantFacade))
                {
                    facilities = facade.FetchFacilities(User.Identity.GetUserId());
                }
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

            IBooking booking;
            try
            {
                using (var facade = (CAD as TenantFacade))
                {
                    booking = facade.CreateBooking(User.Identity.GetUserId(), model.FacilityId, model.StartTime, model.EndTime);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return getResponse();
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
                using (var facade = (CAD as TenantFacade))
                {
                    booking = facade.FetchBooking(User.Identity.GetUserId(), facilityId, bookingId);
                }
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
                using (var facade = (CAD as TenantFacade))
                {
                    bookings = facade.FetchFacilityBookings(User.Identity.GetUserId(), facilityId);
                }
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
                using (var facade = (CAD as TenantFacade))
                {
                    bookings = facade.FetchPersonBookings(User.Identity.GetUserId());
                }
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
                using (var facade = (CAD as TenantFacade))
                {
                    facade.CancelBooking(User.Identity.GetUserId(), model.FacilityId, model.BookingId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return getResponse();
        }

        #endregion

    }
}
