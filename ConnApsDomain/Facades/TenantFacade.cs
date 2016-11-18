using System;
using System.Collections.Generic;
using ConnApsDomain.Models;

namespace ConnApsDomain.Facades
{
    public class TenantFacade: Facade
    {
        #region Tenant

        public ITenant FetchTenant(string userId)
        {
            var tenant = PersonRegister.FetchTenant(userId);
            return tenant;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = PersonRegister.UpdateTenant(userId, firstName, lastName, dob, phone);
            return tenant;
        }

        #endregion

        #region Facility
        
        public IEnumerable<IFacility> FetchFacilities(string userId)
        {
            var buildingId = PersonRegister.FetchTenantBuildingId(userId);
            var facilities = BuildingRegister.FetchFacilities(buildingId);
            return facilities;
        }

        #endregion

        #region Booking

        public IBooking CreateBooking(string userId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var t = PersonRegister.FetchTenant(userId);
            var booking = BuildingRegister.CreateBooking(t.BuildingId, facilityId, t.Id, startTime, endTime);
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(string userId, int facilityId)
        {
            var buildingId = PersonRegister.FetchTenantBuildingId(userId);
            var bookings = BuildingRegister.FetchFacilityBookings(buildingId, facilityId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int facilityId, int bookingId)
        {
            var buildingId = PersonRegister.FetchTenantBuildingId(userId);
            var bookings = BuildingRegister.FetchBooking(buildingId, facilityId, bookingId);
            return bookings;
        }

        public IBooking CancelBooking(string userId, int facilityId, int bookingId)
        {
            var buildingId = PersonRegister.FetchTenantBuildingId(userId);
            var booking = BuildingRegister.DeleteBooking(buildingId, facilityId, bookingId);
            return booking;
        }

        public IEnumerable<IBooking> FetchPersonBookings(string userId)
        {
            var bookings = PersonRegister.FetchPersonBookings(userId);
            return bookings;
        }

        #endregion
    }
}
