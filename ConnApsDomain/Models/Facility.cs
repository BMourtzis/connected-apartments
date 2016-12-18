using System;
using System.Collections.Generic;
using System.Linq;
using ConnApsDomain.Exceptions;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an instance of a facility in the building
    /// Inherets from Location Class
    /// </summary>
    internal sealed class Facility: Location, IFacility
    {
        /// <summary>
        /// A list of bookings that are booked in this facility
        /// </summary>
        internal ICollection<Booking> Bookings { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Facility
        /// Used by Entity Framework
        /// </summary>
        private Facility(): base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">The Level the Facility is on</param>
        /// <param name="number">The Number of the facility</param>
        /// <param name="buildingId">The Id the Building that the facility is a part of</param>
        public Facility(string level, string number, int buildingId) : base(level, number, buildingId) { }

        #endregion

        #region Properties

        #endregion

        #region Functions

        /// <summary>
        /// Creates a new booking and adds it to the Bookings list.
        /// The booking is made for this facility
        /// </summary>
        /// <param name="personId">The id of the person that makes the booking</param>
        /// <param name="startTme">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the bookings ends</param>
        /// <returns>Returns the Booking object</returns>
        public Booking CreateBooking(int personId, DateTime startTme, DateTime endTime)
        {
            var booking = new Booking(Id, personId, startTme, endTime);
            Bookings.Add(booking);
            return booking;
        }
       
        /// <summary>
        /// Fetches a booking made for this facility with the specified ID
        /// </summary>
        /// <param name="bookingId">The ID of the booking</param>
        /// <returns>Returns the booking object with the ID specified</returns>
        public Booking FetchBooking(int bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking");
            }

            return booking;
        }

        /// <summary>
        /// Cancels (Deletes) a booking from this facility with the specified ID
        /// </summary>
        /// <param name="bookingId">The ID of the Booking</param>
        public void CancelBooking(int bookingId)
        {
            var booking = FetchBooking(bookingId);
            Bookings.Remove(booking);
        }

        /// <summary>
        /// Updates the Informatin in this facility
        /// </summary>
        /// <param name="level">The level the facility is on</param>
        /// <param name="number">The number of the facility</param>
        public void UpdateFacility(string level, string number)
        {
            UpdateLocation(level, number);
        }

        #endregion
    }

    public interface IFacility
    {
        /// <summary>
        /// The ID of the facility
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The level the facility is on
        /// </summary>
        string Level { get; }

        /// <summary>
        /// The Number of the Facility
        /// </summary>
        string Number { get; }

        /// <summary>
        /// The ID of the building the facility is in
        /// </summary>
        int BuildingId { get; }
    }
}
