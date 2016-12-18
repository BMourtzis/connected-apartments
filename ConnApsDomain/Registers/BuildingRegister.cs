using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;

namespace ConnApsDomain.Registers
{
    /// <summary>
    /// Connects to the building via the DbContext
    /// </summary>
    internal class BuildingRegister: IDisposable
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// Initialises a new ConnApsContext
        /// </summary>
        public BuildingRegister()
        {
            _context = new ConnApsContext();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Connects to the database.
        /// </summary>
        private readonly ConnApsContext _context;

        #endregion

        #region Building

        /// <summary>
        /// Create a new Building Instance
        /// </summary>
        /// <param name="buildingName">The name of the building</param>
        /// <param name="address">The address that the building is on</param>
        /// <returns>Returns the newly created building</returns>
        public Building CreateBuilding(string buildingName, string address)
        {
            var building = new Building(buildingName, address);
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return building;
        }

        /// <summary>
        /// Updates Building Information
        /// </summary>
        /// <param name="buildingId">The Id of the Building being updated</param>
        /// <param name="buildingName">The New name of the Building</param>
        /// <param name="address">The New Address of the Building</param>
        /// <returns></returns>
        public Building UpdateBuilding(int buildingId, string buildingName, string address)
        {
            var building = FetchBuilding(buildingId);
            building.UpdateBuilding(buildingName, address);
            _context.SaveChanges();
            return building;
        }

        /// <summary>
        /// Fetchs a Building with the supplied ID
        /// </summary>
        /// <param name="buildingId">The ID of the building</param>
        /// <returns>Returns the Building with the corresponding ID</returns>
        public Building FetchBuilding(int buildingId)
        {
            var building = _context.Buildings.FirstOrDefault(b => b.Id.Equals(buildingId));

            if (building == null)
            {
                throw new InternalException();
            }

            return building;
        }

        /// <summary>
        /// Fetches all the building on the Database
        /// </summary>
        /// <returns>A list of buildings</returns>
        public IEnumerable<Building> FetchBuildings()
        {
            var buildings = _context.Buildings;
            return buildings;
        }

        #endregion

        #region Apartment

        /// <summary>
        /// Creates a new apartment
        /// </summary>
        /// <param name="level">The Level the apartment is on</param>
        /// <param name="number">The Number of the apartment</param>
        /// <param name="tenantsAllowed"> The number of tenants allowed</param>
        /// <param name="facingDirection">The Direction the apartment is facing</param>
        /// <param name="buildingId">The ID of the building the apartment is on</param>
        /// <returns>Returns the newly created apartment</returns>
        public Apartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var apt = FetchBuilding(buildingId).CreateApartment(level, number, tenantsAllowed, facingDirection);
            _context.SaveChanges();
            return apt;
        }
        
        /// <summary>
        /// Updates the apartment information
        /// </summary>
        /// <param name="apartmentId">The ID of the apartment being updated</param>
        /// <param name="buildingId">The ID of the building that the apartment is in</param>
        /// <param name="level">The new level of the apartment</param>
        /// <param name="number">The new number of the apartment</param>
        /// <param name="tenantsAllowed">The new number of tenant allowed in the apartment</param>
        /// <param name="facingDirection">The new Direction the apartment is facing</param>
        /// <returns>Returns the newly updated apartment</returns>
        public Apartment UpdateApartment(int apartmentId, int buildingId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            var building = _context.Buildings.Include(b => b.Apartments).FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var apt = building.UpdateApartment(apartmentId, level, number, tenantsAllowed, facingDirection);
            _context.SaveChanges();
            return apt;
        }
        
        /// <summary>
        /// Fetch an apartment object
        /// </summary>
        /// <param name="buildingId">The ID of the Building that the apartment is in</param>
        /// <param name="apartmentId">The ID of the apartment</param>
        /// <returns>Returns the Apartment in search of</returns>
        public Apartment FetchApartment(int buildingId, int apartmentId)
        {
            var building = _context.Buildings.Include(b => b.Locations).FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var apt = building.FetchApartment(apartmentId);
            return apt;
        }

        /// <summary>
        /// Fetchs all the apartment of a building
        /// </summary>
        /// <param name="buildingId">The ID of the building</param>
        /// <returns>Returns a list of Apartments</returns>
        public IEnumerable<Apartment> FetchApartments(int buildingId)
        {
            var building = _context.Buildings.Include(b => b.Apartments)
                                             .FirstOrDefault(b => b.Id == buildingId);
            if (building == null)
            {
                throw new InternalException();
            }

            return building.Apartments;
        }

        /// <summary>
        /// Fetches all the apartments in the building
        /// </summary>
        /// <returns>Returns a list of apartments</returns>
        public IEnumerable<Apartment> FetchApartments()
        {
            var apts = _context.Apartments;
            return apts;
        }

        #endregion

        #region Facility

        /// <summary>
        /// Creates a facility object in a building
        /// </summary>
        /// <param name="buildingId">The Id of the Building the Facility is going to be a part of</param>
        /// <param name="level">The level that the facility is on</param>
        /// <param name="number">The number of facility</param>
        /// <returns>Returns the newly created facility</returns>
        public Facility CreateFacility(int buildingId, string level, string number)
        {
            var facility = FetchBuilding(buildingId).CreateFacility(level, number);
            _context.SaveChanges();
            return facility;
        }

        /// <summary>
        /// Updates the information in a facility
        /// </summary>
        /// <param name="buildingId">The ID of the building the facility is in</param>
        /// <param name="facilityId">The ID of the facility</param>
        /// <param name="level">The new level of the facility</param>
        /// <param name="number">The new number of the facility</param>
        /// <returns>Returns the newly updated Facility</returns>
        public Facility UpdateFacility(int buildingId, int facilityId, string level, string number)
        {
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == buildingId);
            if (building == null)
            {
                throw new InternalException();
            }
            var facility = building.UpdateFacility(facilityId, level, number);
            _context.SaveChanges();
            return facility;
        }

        /// <summary>
        /// Fetches all the facility of the specified building
        /// </summary>
        /// <param name="id">The Id of the Building</param>
        /// <returns>Returns a list of facilities</returns>
        public IEnumerable<Facility> FetchFacilities(int id)
        {
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == id);

            if (building == null)
            {
                throw new InternalException();
            }

            var facilities = building.Facilities;
            return facilities;
        }

        //TODO: Add pagination
        /// <summary>
        /// Fetch a facility
        /// </summary>
        /// <param name="buildingId">The Id of the building the facility is in</param>
        /// <param name="facilityId">The Id of the facility</param>
        /// <returns>Returns the Facility</returns>
        public Facility FetchFacility(int buildingId, int facilityId)
        {
            var building = _context.Buildings.Include(b => b.Locations).FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var facility = building.FetchFacility(facilityId);
            return facility;
        }

        //TODO: Add pagination
        /// <summary>
        /// Fetchs all facilities of the system
        /// </summary>
        /// <returns>Retuns a list of facilities</returns>
        public IEnumerable<Facility> FetchFacilities()
        {
            var facilities = _context.Facilities;
            return facilities;
        }

        #endregion

        #region Booking
        
        /// <summary>
        /// Creates a new booking
        /// </summary>
        /// <param name="buildingId">The Id of the Building the booking is in</param>
        /// <param name="facilityId">The Id of the facility the booking is in</param>
        /// <param name="personId">The Id of the Person the makes the booking</param>
        /// <param name="startTime">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the booking ends</param>
        /// <returns>Returns thew newly created booking</returns>
        public Booking CreateBooking(int buildingId, int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            CheckBookingAvailability(buildingId, facilityId, startTime, endTime);
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var booking = building.CreateBooking(facilityId, personId, startTime, endTime);
            _context.SaveChanges();
            return booking;
        }

        //TODO: Check if there is any better way of doing this.
        /// <summary>
        /// Fetchs a booking
        /// </summary>
        /// <param name="buildingId">The Id of the building</param>
        /// <param name="facilityId">The Id of the facility</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns the booking</returns>
        public Booking FetchBooking(int buildingId, int facilityId, int bookingId)
        {
            var building = _context.Buildings
                                  .Include(b => b.Locations.OfType<Facility>())
                                  .Include(b => b.Facilities.Select(f => f.Bookings))
                                  .FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var booking = building.FetchBooking(bookingId);
            return booking;
        }

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="buildingId">The Id of the building</param>
        /// <param name="facilityId">The Id of the facility</param>
        /// <param name="bookingId">The Id of the booking</param>
        public void CancelBooking(int buildingId, int facilityId, int bookingId)
        {
            var building = _context.Buildings
                                 .Include(b => b.Locations.OfType<Facility>())
                                 .Include(b => b.Facilities.Select(f => f.Bookings))
                                 .FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            building.CancelBooking(facilityId, bookingId);
            _context.SaveChanges();
        }

        //TODO: Add pagination
        /// <summary>
        /// Fetches booking of a facility
        /// </summary>
        /// <param name="buildingId">The Id of the building</param>
        /// <param name="facilityId">The Id of the facility</param>
        /// <returns>Returns a list of bookings</returns>
        public IEnumerable<Booking> FetchBookings(int buildingId, int facilityId)
        {
            var building = _context.Buildings
                                  .Include(b => b.Locations.OfType<Facility>().Select(f => f.Bookings))
                                  .FirstOrDefault(b => b.Id == buildingId);

            if (building == null)
            {
                throw new InternalException();
            }

            var booking = building.FetcBookings(facilityId);
            return booking;
        }

        //TODO: Add pagination
        /// <summary>
        /// Fetches all the booking in the database
        /// </summary>
        /// <returns>Returns a list of bookings</returns>
        public IEnumerable<Booking> FetchBookings()
        {
            var bookings = _context.Bookings;
            return bookings;
        }

        /// <summary>
        /// Checks if the the period of the new booking is available
        /// </summary>
        /// <param name="buildingId">The Id of the building that the booking is in</param>
        /// <param name="facilityId">The Id of the facility that the booking is in</param>
        /// <param name="startTime">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the booking ends</param>
        private void CheckBookingAvailability(int buildingId,int facilityId, DateTime startTime, DateTime endTime)
        {
            var bookings = FetchBookings(buildingId, facilityId);

            if (bookings.Any(booking => (booking.StartTime >= startTime && booking.EndTime < startTime) || (booking.StartTime < endTime && booking.EndTime >= endTime )))
            {
                throw new BookingOverlapingException();
            }
        }

        #endregion

        #region BuildingManager

        /// <summary>
        /// Fetches the Building Manager from the specified Building
        /// </summary>
        /// <param name="id">The Id of the Building</param>
        /// <returns>Returns a list of Building Managers</returns>
        public IEnumerable<BuildingManager> FetchBuildingManagers(int id)
        {
            var building = _context.Buildings.Include(b => b.Managers).FirstOrDefault(b => b.Id == id);

            if (building == null)
            {
                throw new InternalException();
            }

            return building.Managers;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
