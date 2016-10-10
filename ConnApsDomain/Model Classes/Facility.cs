using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Facility: Location, IFacility
    {
        internal virtual ICollection<Booking> Bookings { get; set; }

        #region Constructors

        protected Facility(): base() { }

        public Facility(string Level, string Number, int buildingid) : base(Level, Number, buildingid) { }

        #endregion

        #region Properties

        /// <summary>
        /// Returns all the bookings that have taken place in this facility
        /// </summary>
        IEnumerable<IBooking> IFacility.Bookings
        {
            get
            {
                return Bookings;
            }
        }

        /// <summary>
        /// The Building that the apartment is in
        /// </summary>
        IBuilding IFacility.Building
        {
            get
            {
                return Building;
            }
        }

        #endregion

        #region Functions

        public Booking CreateBooking(int personId, DateTime startTme, DateTime endTime)
        {
            var b = new Booking(Id, personId, startTme, endTime);
            return b;
        }

        public Booking FetchBooking(int BookingId)
        {
            var booking = Bookings.Where(b => b.Id == BookingId).FirstOrDefault();
            return booking;
        }

        public void UpdateFacility(string level, string number)
        {
            UpdateLocation(level, number);
        }

        #endregion
    }
}
