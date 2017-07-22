using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ConnApsDomain.Exceptions;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an instance of a Building
    /// </summary>
    internal class Building: IBuilding
    {
        /// <summary>
        /// A list of all the Location that belong to this Building
        /// </summary>
        internal ICollection<Location> Locations { get; set; }

        /// <summary>
        /// A list of the managers of the building
        /// </summary>
        internal ICollection<BuildingManager> Managers { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Building
        /// Used by Entity Framework
        /// </summary>
        protected Building() { }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="buildingName">The Name of the building</param>
        /// <param name="address">The address of the building</param>
        public Building(string buildingName, string address)
        {
            BuildingName = buildingName;
            Address = address;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the Building
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The name of the building
        /// </summary>
        [Required]
        public string BuildingName { get; private set; }

        /// <summary>
        /// The address that the building is on
        /// </summary>
        [Required]
        public string Address { get; private set; }

        /// <summary>
        /// A list of the Apartments that belong to the Building
        /// </summary>
        public IEnumerable<Apartment> Apartments => Locations?.OfType<Apartment>();

        /// <summary>
        /// A list of the Facilities that belong to the Building
        /// </summary>
        public IEnumerable<Facility> Facilities => Locations?.OfType<Facility>();

        #endregion

        #region Methods

        /// <summary>
        /// Update information for the building
        /// </summary>
        /// <param name="buildingName">The name of the building</param>
        /// <param name="address">The address the building is on</param>
        public void UpdateBuilding(string buildingName, string address)
        {
            BuildingName = buildingName;
            Address = address;
        }

        /// <summary>
        /// Create a new apartment instance to be part of the building
        /// </summary>
        /// <param name="level">The level that the apartment is on</param>
        /// <param name="number">The number of the apartment (Location)</param>
        /// <param name="tenantsAllowed">The number of tenants allowed</param>
        /// <param name="facingDirection">The Direction the apartment is facing</param>
        /// <returns>Returns the new apartment instance</returns>
        public Apartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection)
        {
            var apt = new Apartment(level, number, tenantsAllowed, facingDirection, Id);
            Locations.Add(apt);
            return apt;
        }

        /// <summary>
        /// Fetches an apartment within the building with the supplied ID
        /// </summary>
        /// <param name="aptId">The ID of the apartment</param>
        /// <returns>The instance of the apartment with the supplied ID</returns>
        public Apartment FetchApartment(int aptId)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == aptId);

            if (apartment == null)
            {
                throw new NotFoundException("Apartment");
            }

            return apartment;
        }

        /// <summary>
        /// Updates the information stores on the apartment instance
        /// </summary>
        /// <param name="apartmentId">The ID of the apartment, used to fetch the apartment</param>
        /// <param name="level">The level the apartment is on</param>
        /// <param name="number">The number of the apartment</param>
        /// <param name="tenantsAllowed">The number of tenants allowed to live in the apartment</param>
        /// <param name="facingDirection">The direction the apartment is facing</param>
        /// <returns></returns>
        public Apartment UpdateApartment(int apartmentId, string level, string number, int tenantsAllowed,
            string facingDirection)
        {
            var apt = FetchApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        /// <summary>
        /// Creates a new Facility instance to be part of the Building
        /// </summary>
        /// <param name="level">The level the Facility is on</param>
        /// <param name="number">The number of the facility within the building</param>
        /// <returns></returns>
        public Facility CreateFacility(string level, string number)
        {
            var f = new Facility(level, number, Id);
            Locations.Add(f);
            return f;
        }

        /// <summary>
        /// Fetches the facility with the supplied ID within the building
        /// </summary>
        /// <param name="facilityId">The ID of the Facility</param>
        /// <returns>The object of the Facility in search of</returns>
        public Facility FetchFacility(int facilityId)
        {
            var facility = Facilities.FirstOrDefault(f => f.Id == facilityId);

            if (facility == null)
            {
                throw new NotFoundException("Facility");    
            }

            return facility;
        }

        /// <summary>
        /// Update the information of the facility object
        /// </summary>
        /// <param name="facilityId">The ID of the Facility. Used to fetch the facility</param>
        /// <param name="level">The level the facility is ons</param>
        /// <param name="number">The number of the facility</param>
        /// <returns></returns>
        public Facility UpdateFacility(int facilityId, string level, string number)
        {
            var facility = FetchFacility(facilityId);
            facility.UpdateFacility(level, number);
            return facility;
        }

        /// <summary>
        /// Creates a new Booking in the specified facility which is part of the building
        /// </summary>
        /// <param name="facilityId">The ID of the facility that the booking is for</param>
        /// <param name="personId">The ID of the person that made the booking</param>
        /// <param name="startTime">The Datetime the booking starts</param>
        /// <param name="endTime">The Datetime the booking ends</param>
        /// <returns>Returns the newly created instance of the booking</returns>
        public Booking CreateBooking(int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            var facility = FetchFacility(facilityId);
            var booking = facility.CreateBooking(personId, startTime, endTime);
            return booking;
        }

        /// <summary>
        /// Fetches a booking with the supplied ID.
        /// The facility the booking is for needs to be part of the building.
        /// </summary>
        /// <param name="bookingId">The ID of the booking</param>
        /// <returns>Returns the booking object in search of</returns>
        public Booking FetchBooking(int bookingId)
        {
            //TODO:Need to test this. Most probably an exception will be thrown
            var booking = Facilities.Select(f => f.Bookings.FirstOrDefault(b => b.Id == bookingId)).FirstOrDefault();
            //var booking = Facilities.FirstOrDefault(f => f.Id == facilityId).FetchBooking(bookingId);
            return booking;
        }

        /// <summary>
        /// Fetches a list of booking for a specific facility
        /// </summary>
        /// <param name="facilityId">The ID of the facility</param>
        /// <returns>Returns a list of bookings</returns>
        public IEnumerable<Booking> FetcBookings(int facilityId)
        {
            var facility = FetchFacility(facilityId);
            return facility.Bookings;
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="facilityId">The ID of the facility the booking is for</param>
        /// <param name="bookingId">The ID of the booking</param>
        public void CancelBooking(int facilityId, int bookingId)
        {
            var facility = FetchFacility(facilityId);
            facility.CancelBooking(bookingId);
        }

        #endregion


    }

    public interface IBuilding
    {
        /// <summary>
        /// The ID of the Building
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The Name of the Building
        /// </summary>
        string BuildingName { get; }

        /// <summary>
        /// The Address of the Building
        /// </summary>
        string Address { get; }
    }
}
