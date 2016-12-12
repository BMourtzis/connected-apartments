using System;
using System.Collections.Generic;
using System.Linq;
using ConnApsDomain.Exceptions;

namespace ConnApsDomain.Models
{
    internal sealed class Facility: Location, IFacility
    {
        internal ICollection<Booking> Bookings { get; set; }

        #region Constructors

        private Facility(): base() { }

        public Facility(string level, string number, int buildingId) : base(level, number, buildingId) { }

        #endregion

        #region Properties

        #endregion

        #region Functions

        public Booking CreateBooking(int personId, DateTime startTme, DateTime endTime)
        {
            var booking = new Booking(Id, personId, startTme, endTime);
            Bookings.Add(booking);
            return booking;
        }

        public Booking FetchBooking(int bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking");
            }

            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            var booking = FetchBooking(bookingId);
            Bookings.Remove(booking);
        }

        public void UpdateFacility(string level, string number)
        {
            UpdateLocation(level, number);
        }

        #endregion
    }

    public interface IFacility
    {
        int Id { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
    }
}
