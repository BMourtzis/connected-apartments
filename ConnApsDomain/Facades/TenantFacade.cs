using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class TenantFacade: Facade
    {
        #region Tenant

        public ITenant FetchTenant(string userId)
        {
            var tenant = personRegister.FetchTenant(userId);
            return tenant;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = personRegister.UpdateTenant(userId, firstName, lastName, dob, phone);
            return tenant;
        }

        #endregion

        #region Booking

        public IBooking CreateBooking(string userId, int FacilityId, DateTime StartTime, DateTime EndTime)
        {
            var t = personRegister.FetchTenant(userId);
            var booking = buildingRegister.CreateBooking(t.BuildingId, FacilityId, t.Id, StartTime, EndTime);
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(string userId, int FacilityId)
        {
            var buildingId = personRegister.FetchTenantBuildingId(userId);
            var bookings = buildingRegister.FetchFacilityBookings(buildingId, FacilityId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int FacilityId, int BookingId)
        {
            var buildingId = personRegister.FetchTenantBuildingId(userId);
            var bookings = buildingRegister.FetchBooking(buildingId, FacilityId, BookingId);
            return bookings;
        }

        public IBooking CancelBooking(string userId, int FacilityId, int BookingId)
        {
            var buildingId = personRegister.FetchTenantBuildingId(userId);
            var booking = buildingRegister.DeleteBooking(buildingId, FacilityId, BookingId);
            return booking;
        }

        public IEnumerable<IBooking> FetchPersonBookings(string userId)
        {
            var bookings = personRegister.FetchPersonBookings(userId);
            return bookings;
        }

        #endregion

        public IEnumerable<IPerson> GetBuildingPeople(string userId)
        {
            var buildingId = personRegister.FetchTenantBuildingId(userId);
            List<IPerson> people = new List<IPerson>();
            people.AddRange(personRegister.FetchBuildingTenants(buildingId));
            people.AddRange(personRegister.FetchBuildingBuildingManager(buildingId));
            return people;
        }
    }
}
