using System;
using System.Collections.Generic;
using System.Linq;

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
            return new Booking(Id, personId, startTme, endTime);
        }

        public Booking FetchBooking(int bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            return booking;
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
