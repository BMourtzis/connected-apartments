using System;
using System.Collections.Generic;
using System.Web.Http;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;
using ConnApsWebAPI.Models;
using Microsoft.AspNet.Identity;

namespace ConnApsWebAPI.Controllers
{
    [Authorize, RoutePrefix("api/Bookings")]
    public class BookingController : BaseController
    {
        //GET api/Booking
        [HttpGet, Route()]
        public IHttpActionResult FetchBooking(int bookingId)
        {
            IBooking booking;
            try
            {
                booking = Cad.FetchBooking(User.Identity.GetUserId(), bookingId);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IBooking>(booking);
        }

        //GET api/Booking
        [HttpGet, Route()]
        public IHttpActionResult FetchBooking(int facilityId, int bookingId)
        {
            IBooking booking;
            try
            {
                booking = Cad.FetchBooking(User.Identity.GetUserId(), facilityId, bookingId);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok<IBooking>(booking);
        }

        //GET api/Booking
        [HttpGet, Route()]
        public IHttpActionResult FetchFacilityBookings(int facilityId)
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId(), facilityId);
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
           
            return Ok<IEnumerable<IBooking>>(bookings);
        }

        //GET api/Booking
        [HttpGet, Route()]
        public IHttpActionResult FetchPersonBookings()
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId());
            }
            catch (ConnectedApartmentsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok<IEnumerable<IBooking>>(bookings);
        }

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

        //DELETE api/Booking/Cancel
        [HttpDelete, Route("Cancel")]
        public IHttpActionResult CancelBooking(BookingCancelModel model)
        {
            try
            {
                Cad.CancelBooking(User.Identity.GetUserId(), model.BookingId);
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

        //DELETE api/Booking/Cancel
        [Authorize(Roles = "BuildingManager"), HttpDelete, Route("Cancel")]
        public IHttpActionResult CancelBooking(BMBookingCancelModel model)
        {
            try
            {
                Cad.CancelBooking(User.Identity.GetUserId(), model.FacilityId, model.BookingId);
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
