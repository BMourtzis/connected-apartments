using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ConnApsDomain.Exceptions;

namespace ConnApsDomain.Models
{
    internal class Building: IBuilding
    {
        internal ICollection<Location> Locations { get; set; }
        internal ICollection<BuildingManager> Managers { get; set; }

        #region Constructors

        protected Building() { }

        public Building(string buildingname, string newAddress)
        {
            BuildingName = buildingname;
            Address = newAddress;
        }

        #endregion

        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string BuildingName { get; private set; }

        [Required]
        public string Address { get; private set; }

        public IEnumerable<Apartment> Apartments => Locations?.OfType<Apartment>();

        public IEnumerable<Facility> Facilities => Locations?.OfType<Facility>();

        #endregion

        #region Functions

        public void UpdateBuilding(string buildingName, string address)
        {
            BuildingName = buildingName;
            Address = address;
        }

        public Apartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection)
        {
            var apt = new Apartment(level, number, tenantsAllowed, facingDirection, Id);
            Locations.Add(apt);
            return apt;
        }

        public Apartment FetchApartment(int aptId)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == aptId);

            if (apartment == null)
            {
                throw new NotFoundException("Apartment");
            }

            return apartment;
        }

        public Apartment UpdateApartment(int apartmentId, string level, string number, int tenantsAllowed,
            string facingDirection)
        {
            var apt = FetchApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        public Facility CreateFacility(string level, string number)
        {
            var f = new Facility(level, number, Id);
            Locations.Add(f);
            return f;
        }

        public Facility FetchFacility(int facilityId)
        {
            var facility = Facilities.FirstOrDefault(f => f.Id == facilityId);

            if (facility == null)
            {
                throw new NotFoundException("Facility");    
            }

            return facility;
        }

        public Facility UpdateFacility(int facilityId, string level, string number)
        {
            var facility = FetchFacility(facilityId);
            facility.UpdateFacility(level, number);
            return facility;
        }

        public Booking CreateBooking(int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            var facility = FetchFacility(facilityId);
            var booking = facility.CreateBooking(personId, startTime, endTime);
            return booking;
        }

        public Booking FetchBooking(int bookingId)
        {
            //Need to test this. Most probably an exception will be thrown
            var booking = Facilities.Select(f => f.Bookings.FirstOrDefault(b => b.Id == bookingId)).FirstOrDefault();
            //var booking = Facilities.FirstOrDefault(f => f.Id == facilityId).FetchBooking(bookingId);
            return booking;
        }

        public IEnumerable<Booking> FetcBookings(int facilityId)
        {
            var facility = FetchFacility(facilityId);
            return facility.Bookings;
        }

        public void CancelBooking(int facilityId, int bookingId)
        {
            var facility = FetchFacility(facilityId);
            facility.CancelBooking(bookingId);
        }

        #endregion


    }

    public interface IBuilding
    {
        int Id { get; }
        string BuildingName { get; }
        string Address { get; }
    }
}
