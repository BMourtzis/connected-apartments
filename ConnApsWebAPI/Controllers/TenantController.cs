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
        protected TenantFacade CAD;

        public TenantController()
        {
            CAD = new TenantFacade();
        }

        #region Tenant

        // GET api/BuildingManager/TenantInfo
        [HttpGet]
        [Route("TenantInfo")]
        public Response<ITenant> FetchTenant()
        {
            ITenant t;
            try
            {
                t = CAD.FetchTenant(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<ITenant>(e.Message);
            }
            return getResponse<ITenant>(t);
        }

        // PUT api/BuildingManager/UpdateTenant
        [HttpPut]
        [Route("UpdateTenant")]
        public GenericResponse UpdateTenant(TenantUpdateModel model)
        {
            ITenant t;
            try
            {
                t = CAD.UpdateTenant(User.Identity.GetUserId(), model.FirstName, model.LastName, model.DoB, model.Phone);
            }
            catch (Exception e)
            {
                return getBadResponse(e.Message);
            }
            return getResponse();
        }

        #endregion

        #region Facility

        [HttpGet]
        [Route("FetchFacilities")]
        public Response<IEnumerable<IFacility>> FetchFacilities()
        {
            IEnumerable<IFacility> facilities;
            try
            {
                facilities = CAD.FetchFacilities(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<IEnumerable<IFacility>>(e.Message);
            }
            return getResponse<IEnumerable<IFacility>>(facilities);
        }

        #endregion

        #region Booking

        [HttpPost]
        [Route("CreateBooking")]
        public GenericResponse CreateBooking(BookingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return getBadResponse(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }

            IBooking booking;
            try
            {
                booking = CAD.CreateBooking(User.Identity.GetUserId(), model.FacilityId, model.StartTime, model.EndTime);
            }
            catch (Exception e)
            {
                return getBadResponse(e.Message);
            }


            return getResponse();
        }

        [HttpGet]
        [Route("FetchBooking")]
        public Response<IBooking> FetchBooking(int facilityId, int bookingId)
        {
            IBooking booking;
            try
            {
                booking = CAD.FetchBooking(User.Identity.GetUserId(), facilityId, bookingId);
            }
            catch (Exception e)
            {
                return getBadResponse<IBooking>(e.Message);
            }
            return getResponse<IBooking>(booking);
        }

        [HttpGet]
        [Route("FetchFacilityBookings")]
        public Response<IEnumerable<IBooking>> FetchFacilityBookings(int facilityId)
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = CAD.FetchFacilityBookings(User.Identity.GetUserId(), facilityId);
            }
            catch (Exception e)
            {
                return getBadResponse<IEnumerable<IBooking>>(e.Message);
            }
            return getResponse<IEnumerable<IBooking>>(bookings);
        }

        [HttpGet]
        [Route("FetchPersonBookings")]
        public Response<IEnumerable<IBooking>> FetchPersonBookings()
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = CAD.FetchPersonBookings(User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return getBadResponse<IEnumerable<IBooking>>(e.Message);
            }
            return getResponse<IEnumerable<IBooking>>(bookings);
        }

        [HttpDelete]
        [Route("CancelBooking")]
        public GenericResponse CancelBooking(int FacilityId, int BookingId)
        {
            IBooking bookings;
            try
            {
                bookings = CAD.CancelBooking(User.Identity.GetUserId(), FacilityId, BookingId);
            }
            catch (Exception e)
            {
                return getBadResponse(e.Message);
            }
            return getResponse();
        }

        #endregion

    }
}
