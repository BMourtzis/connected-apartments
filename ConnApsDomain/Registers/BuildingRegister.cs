using ConnApsDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingRegister: IDisposable
    {
        #region Constructors

        public BuildingRegister() { }

        #endregion

        #region Properties

        ConnApsContext context = new ConnApsContext();

        #endregion

        #region Building

        public IBuilding CreateBuilding(string buildingName, string address)
        {
            Building building = new Building(buildingName, address);
            context.Buildings.Add(building);
            context.SaveChanges();
            return building;
        }

        public IBuilding UpdateBuilding(int buildingId, string buildingName, string address)
        {
            Building building = getBuilding(buildingId);
            building.UpdateBuilding(buildingName, address);
            context.SaveChanges();
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            Building bu = context.Buildings
                                 .Include("Locations")
                                 .Include("Managers")
                                 .Where(b => b.Id.Equals(buildingId))
                                 .FirstOrDefault();
            return bu;
        }

        private Building getBuilding(int buildingId)
        {
            Building bu = context.Buildings
                                 .Where(b => b.Id.Equals(buildingId))
                                 .FirstOrDefault();
            return bu;
        }

        public IEnumerable<IApartment> GetBuildingApartments(int buildingId)
        {
            var building = FetchBuilding(buildingId);
            return building.Apartments;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, int buildingId)
        {
            var building = getBuilding(buildingId);
            Apartment apt = building.CreateApartment(level, number, tenantsAllowed, facingDirection);
            context.Apartments.Add(apt);
            context.SaveChanges();
            return apt;
        }

        public IApartment UpdateApartment(int apartmentId, int buildingId, string level, string number, int tenantsAllowed, string facingDirection)
        {
            Building building = context.Buildings.Include("Locations")
                                                 .Where(b => b.Id == buildingId)
                                                 .FirstOrDefault();
            Apartment apt = building.FetchApartment(apartmentId);
            apt.UpdateApartment(level, number, tenantsAllowed, facingDirection);
            context.SaveChanges();
            return apt;
        }

        public IApartment FetchApartment(int buildingId, int apartmentId)
        {
            Building building = context.Buildings.Include("Locations")
                                                 .Where(b => b.Id == buildingId)
                                                 .FirstOrDefault();
            Apartment apt = building.FetchApartment(apartmentId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(int buildingId)
        {
            var b = FetchBuilding(buildingId);
            return b.Apartments;
        }

        private Apartment getApartment(int apartmentId)
        {
            Apartment apt = context.Apartments
                                   .Where(a => a.Id.Equals(apartmentId))
                                   .FirstOrDefault();
            return apt;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion

        #region Facility

        public IFacility CreateFacility(int BuildingId, string Level, string Number)
        {
            var building = getBuilding(BuildingId);
            var facility = building.CreateFacility(Level, Number);
            context.Facilities.Add(facility);
            context.SaveChanges();
            return facility;
        }

        public IFacility UpdateFacility(int BuildingId, int FacilityId, string Level, string Number)
        {
            var building = context.Buildings
                      .Include("Locations")
                      .Where(b => b.Id == BuildingId)
                      .FirstOrDefault();
            var facility = building.UpdateFacility(FacilityId, Level, Number);
            context.SaveChanges();
            return facility;
        }


        public IEnumerable<IFacility> FetchFacilities(int BuildingId)
        {
            var building = context.Buildings.Include("Locations")
                                            .Where(b => b.Id == BuildingId)
                                            .FirstOrDefault();
            var facilities = building.Facilities;
            return facilities;
        }

        public IFacility FetchFacility(int BuildingId, int FacilityId)
        {
            var building = context.Buildings
                                  .Include("Locations")
                                  .Where(b => b.Id == BuildingId)
                                  .FirstOrDefault();
            var facility = building.FetchFacility(FacilityId);
            return facility;
        }

        #endregion

        #region Booking
        
        public IBooking CreateBooking(int BuildingId, int FacilityId, int PersonId, DateTime StartTime, DateTime EndTime)
        {
            CheckBookingAvailability(BuildingId, FacilityId, StartTime, EndTime);
            var building = context.Buildings
                      .Include("Locations")
                      .Where(b => b.Id == BuildingId)
                      .FirstOrDefault();
            var booking = building.CreateBooking(FacilityId, PersonId, StartTime, EndTime);
            context.Bookings.Add(booking);
            context.SaveChanges();
            return booking;
        }

        public IBooking FetchBooking(int BuildingId, int FacilityId, int BookingId)
        {
            var facilty = context.Facilities
                                  .Include("Bookings")
                                  .Where(f => f.BuildingId == BuildingId)
                                  .Where(f => f.Id == FacilityId)
                                  .FirstOrDefault();

            var booking = facilty.FetchBooking(BookingId);
            return booking;
        }

        public IBooking DeleteBooking(int BuildingId, int FacilityId, int BookingId)
        {
            var booking = (Booking)FetchBooking(BuildingId, FacilityId, BookingId);
            context.Bookings.Remove(booking);
            context.SaveChanges();
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(int BuildingId, int FacilityId)
        {
            var facilty = context.Facilities
                                  .Include("Bookings")
                                  .Where(f => f.BuildingId == BuildingId)
                                  .Where(f => f.Id == FacilityId)
                                  .FirstOrDefault();

            var booking = facilty.Bookings;
            return booking;
        }

        private void CheckBookingAvailability(int buildingId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var facilty = context.Facilities
                      .Include("Bookings")
                      .Where(f => f.BuildingId == buildingId)
                      .Where(f => f.Id == facilityId)
                      .FirstOrDefault();

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
    }
}
