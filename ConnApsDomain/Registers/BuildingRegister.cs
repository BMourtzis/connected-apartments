using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;

namespace ConnApsDomain.Registers
{
    internal class BuildingRegister: IDisposable
    {
        #region Constructors

        public BuildingRegister()
        {
            _context = new ConnApsContext();
        }

        #endregion

        #region Properties

        private readonly ConnApsContext _context;

        #endregion

        #region Building

        public Building CreateBuilding(string buildingName, string address)
        {
            var building = new Building(buildingName, address);
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return building;
        }

        public Building UpdateBuilding(int buildingId, string buildingName, string address)
        {
            var building = FetchBuilding(buildingId);
            building.UpdateBuilding(buildingName, address);
            _context.SaveChanges();
            return building;
        }

        public Building FetchBuilding(int buildingId)
        {
            var building = _context.Buildings.FirstOrDefault(b => b.Id.Equals(buildingId));
            return building;
        }

        public IEnumerable<Building> FetchBuildings()
        {
            var buildings = _context.Buildings;
            return buildings;
        }

        #endregion

        #region Apartment

        public Apartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var apt = FetchBuilding(buildingId).CreateApartment(level, number, tenantsAllowed, facingDirection);
            _context.Apartments.Add(apt);
            _context.SaveChanges();
            return apt;
        }

        public Apartment UpdateApartment(int apartmentId, int buildingId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            var building = _context.Buildings.Include(b => b.Apartments).FirstOrDefault(b => b.Id == buildingId);
            var apt = building.FetchApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            _context.SaveChanges();
            return apt;
        }

        public Apartment FetchApartment(int buildingId, int apartmentId)
        {
            var building = _context.Buildings.Include(b => b.Locations).FirstOrDefault(b => b.Id == buildingId);
            var apt = building.FetchApartment(apartmentId);
            return apt;
        }

        public IEnumerable<Apartment> FetchApartments(int buildingId)
        {
            var building = _context.Buildings.Include(b => b.Apartments)
                                             .FirstOrDefault(b => b.Id == buildingId);
            return building.Apartments;
        }

        public IEnumerable<Apartment> FetchApartments()
        {
            var apts = _context.Apartments;
            return apts;
        }

        #endregion

        #region Facility

        public Facility CreateFacility(int buildingId, string level, string number)
        {
            var facility = FetchBuilding(buildingId).CreateFacility(level, number);
            _context.Facilities.Add(facility);
            _context.SaveChanges();
            return facility;
        }

        public Facility UpdateFacility(int buildingId, int facilityId, string level, string number)
        {
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == buildingId);
            var facility = building.UpdateFacility(facilityId, level, number);
            _context.SaveChanges();
            return facility;
        }

        public IEnumerable<Facility> FetchFacilities(int buildingId)
        {
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == buildingId);
            var facilities = building.Facilities;
            return facilities;
        }

        public Facility FetchFacility(int buildingId, int facilityId)
        {
            var building = _context.Buildings.Include(b => b.Locations).FirstOrDefault(b => b.Id == buildingId);
            var facility = building.FetchFacility(facilityId);
            return facility;
        }

        public IEnumerable<Facility> FetchFacilities()
        {
            var facilities = _context.Facilities;
            return facilities;
        }

        #endregion

        #region Booking
        
        public Booking CreateBooking(int buildingId, int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            CheckBookingAvailability(buildingId, facilityId, startTime, endTime);
            var building = _context.Buildings.Include(b => b.Facilities).FirstOrDefault(b => b.Id == buildingId);
            var booking = building.CreateBooking(facilityId, personId, startTime, endTime);
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public Booking FetchBooking(int buildingId, int facilityId, int bookingId)
        {
            var building = _context.Buildings
                                  .Include(b => b.Locations.OfType<Facility>())
                                  .Include(b => b.Facilities.Select(f => f.Bookings))
                                  .FirstOrDefault(b => b.Id == buildingId);

            var booking = building.FetchBooking(bookingId);
            return booking;
        }

        public void CancelBooking(int buildingId, int facilityId, int bookingId)
        {
            var building = _context.Buildings
                                 .Include(b => b.Locations.OfType<Facility>())
                                 .Include(b => b.Facilities.Select(f => f.Bookings))
                                 .FirstOrDefault(b => b.Id == buildingId);
            building.CancelBooking(facilityId, bookingId);
            _context.SaveChanges();
        }

        public IEnumerable<Booking> FetchBookings(int buildingId, int facilityId)
        {
            var building = _context.Buildings
                                  .Include(b => b.Locations.OfType<Facility>().Select(f => f.Bookings))
                                  .FirstOrDefault(b => b.Id == buildingId);

            var booking = building.FetcBookings(facilityId);
            return booking;
        }

        public IEnumerable<Booking> FetchBookings()
        {
            var bookings = _context.Bookings;
            return bookings;
        }

        private void CheckBookingAvailability(int buildingId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var bookings = FetchBookings(buildingId, facilityId);

            if (bookings.Any(booking => (booking.StartTime >= startTime && booking.EndTime < startTime) || (booking.StartTime < endTime && booking.EndTime >= endTime )))
            {
                throw new BookingOverlapingException();
            }
        }

        #endregion

        #region BuildingManager

        public IEnumerable<BuildingManager> FetchBuildingManagers(int buildingId)
        {
            var bms = _context.Buildings.Include(b => b.Managers).FirstOrDefault(b => b.Id == buildingId);
            return bms.Managers;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
