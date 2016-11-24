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
    [Authorize, RoutePrefix("api/Bookings")]
    public class BookingController : BaseController
    {
        //POST api/Bookings/Create
        [HttpPost, Route("Create")]
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

        //GET api/Booking
        [HttpGet]
        public IHttpActionResult FetchBooking(int facilityId, int bookingId) //Change that to only bookingId
        {
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

        //GET api/Booking
        [HttpGet]
        public IHttpActionResult FetchFacilityBookings(int facilityId)
        {
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

        //GET api/Booking
        [HttpGet]
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

        //GET api/Booking/Cancel
        [HttpDelete, Route("Cancel")]
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
    }
}
