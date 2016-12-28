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
    /// This controller is responsible for all the function of the Booking Class
    /// </summary>
    
    [Authorize, RoutePrefix("api/v1/Bookings")]
    public class BookingController : BaseController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BookingController(): base (){ }

        /// <summary>
        /// Constructor that allowes for Dependency Injection
        /// </summary>
        /// <param name="facade"></param>
        public BookingController(IFacade facade): base(facade) { }

        /// <summary>
        /// Fetches the a booking
        /// </summary>
        /// <param name="id">The Id of the booking</param>
        /// <returns>Returns the booking details or an error message</returns>

        //GET api/Booking
        [HttpGet, Route()]
        public IHttpActionResult FetchBooking(int id)
        {
            IBooking booking;
            try
            {
                booking = Cad.FetchBooking(User.Identity.GetUserId(), id);
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

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="facilityId">The id of the facility</param>
        /// <param name="bookingId">The id of the booking</param>
        /// <returns>Returns the booking details or an error message</returns>

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

        /// <summary>
        /// Fetches the booking of a facility
        /// </summary>
        /// <param name="id">The Id of the facility</param>
        /// <returns>Returns a list of the booking details or an error message</returns>

        //GET api/Booking/Facility
        [HttpGet, Route("Facility")]
        public IHttpActionResult FetchFacilityBookings(int id)
        {
            IEnumerable<IBooking> bookings;
            try
            {
                bookings = Cad.FetchBookings(User.Identity.GetUserId(), id);
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

        /// <summary>
        /// Fetches all the bookings of a Person
        /// </summary>
        /// <returns>Returns a list of the booking details or an error message</returns>

        //TODO: Add pagination
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

        /// <summary>
        /// Creates a new Booking
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response an error message</returns>

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

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

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

        /// <summary>
        /// Cancels (Deletes) a booking.
        /// Used by a Building Manager
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns a default response or an error message</returns>

        //DELETE api/Booking/Cancel
        [Authorize(Roles = "BuildingManager"), HttpDelete, Route("Cancel")]
        public IHttpActionResult CancelBooking(BmBookingCancelModel model)
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
