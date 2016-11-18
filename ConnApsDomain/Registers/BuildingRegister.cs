using System;
using System.Collections.Generic;
using System.Linq;
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

        public IBuilding CreateBuilding(string buildingName, string address)
        {
            var building = new Building(buildingName, address);
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return building;
        }

        public IBuilding UpdateBuilding(int buildingId, string buildingName, string address)
        {
            var building = GetBuilding(buildingId);
            building.UpdateBuilding(buildingName, address);
            _context.SaveChanges();
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = _context.Buildings.FirstOrDefault(b => b.Id.Equals(buildingId));
            return building;
        }

        private Building GetBuilding(int buildingId)
        {
            var bu = _context.Buildings.FirstOrDefault(b => b.Id.Equals(buildingId));
            return bu;
        }

        public IEnumerable<IApartment> GetBuildingApartments(int buildingId)
        {
            throw new NotImplementedException();
            //var building = FetchBuilding(buildingId);
            //return building.Apartments;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var apt = GetBuilding(buildingId).CreateApartment(level, number, tenantsAllowed, facingDirection);
            _context.Apartments.Add(apt);
            _context.SaveChanges();
            return apt;
        }

        public IApartment UpdateApartment(int apartmentId, int buildingId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var apt = building.FetchApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            _context.SaveChanges();
            return apt;
        }

        public IApartment FetchApartment(int buildingId, int apartmentId)
        {
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var apt = building.FetchApartment(apartmentId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(int buildingId)
        {
            throw new NotImplementedException();
            //var b = FetchBuilding(buildingId);
            //return b.Apartments;
        }

        private Apartment GetApartment(int apartmentId)
        {
            var apt = _context.Apartments.FirstOrDefault(a => a.Id.Equals(apartmentId));
            return apt;
        }

        #endregion

        #region Facility

        public IFacility CreateFacility(int buildingId, string level, string number)
        {
            var building = GetBuilding(buildingId);
            var facility = building.CreateFacility(level, number);
            _context.Facilities.Add(facility);
            _context.SaveChanges();
            return facility;
        }

        public IFacility UpdateFacility(int buildingId, int facilityId, string level, string number)
        {
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var facility = building.UpdateFacility(facilityId, level, number);
            _context.SaveChanges();
            return facility;
        }


        public IEnumerable<IFacility> FetchFacilities(int buildingId)
        {
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var facilities = building.Facilities;
            return facilities;
        }

        public IFacility FetchFacility(int buildingId, int facilityId)
        {
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var facility = building.FetchFacility(facilityId);
            return facility;
        }

        #endregion

        #region Booking
        
        public IBooking CreateBooking(int buildingId, int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            CheckBookingAvailability(buildingId, facilityId, startTime, endTime);
            var building = _context.Buildings.Include("Locations").FirstOrDefault(b => b.Id == buildingId);
            var booking = building.CreateBooking(facilityId, personId, startTime, endTime);
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public IBooking FetchBooking(int buildingId, int facilityId, int bookingId)
        {
            var facilty = _context.Facilities
                                  .Include("Bookings")
                                  .Where(f => f.BuildingId == buildingId)
                                  .FirstOrDefault(f => f.Id == facilityId);

            var booking = facilty.FetchBooking(bookingId);
            return booking;
        }

        public IBooking DeleteBooking(int buildingId, int facilityId, int bookingId)
        {
            var booking = (Booking)FetchBooking(buildingId, facilityId, bookingId);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(int buildingId, int facilityId)
        {
            var facilty = _context.Facilities
                                  .Include("Bookings")
                                  .Where(f => f.BuildingId == buildingId)
                                  .FirstOrDefault(f => f.Id == facilityId);

            var booking = facilty.Bookings;
            return booking;
        }

        private void CheckBookingAvailability(int buildingId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var facilty = _context.Facilities
                                  .Include("Bookings")
                                  .Where(f => f.BuildingId == buildingId)
                                  .FirstOrDefault(f => f.Id == facilityId);

            var bookings = facilty.Bookings;
            foreach(var booking in bookings)
            {
                if((booking.StartTime >= startTime && booking.EndTime < startTime) || (booking.StartTime < endTime && booking.EndTime >= endTime ))
                {
                    throw new BookingOverlapingException();
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
