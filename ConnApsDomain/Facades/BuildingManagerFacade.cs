using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public class BuildingManagerFacade: Facade
    {

        #region BuildingManager

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var bm = personRegister.FetchBuildingManager(userId);
            return bm;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var bm = personRegister.UpdateBuildingManager(userId, firstname, lastname, dateofbirth, newPhone);
            return bm;
        }

        public int GetBuildingId(string userId)
        {
            var id = FetchBuildingManager(userId).BuildingId;
            return id;
        }

        #endregion

        #region Building

        public IBuilding UpdateBuilding(string userId, string buildingName, string address)
        {
            var building = buildingRegister.UpdateBuilding(personRegister.FetchBuildingManagerBuildingId(userId), buildingName, address);
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = buildingRegister.FetchBuilding(buildingId);
            return building;
        }

        public IBuilding GetTenantBuilding(string userId)
        {
            var building = buildingRegister.FetchBuilding(personRegister.FetchTenant(userId).BuildingId);
            return building;
        }

        public IBuilding FetchBuildingManagerBuilding(string userId)
        {
            var building = buildingRegister.FetchBuilding(personRegister.FetchBuildingManagerBuildingId(userId));
            return building;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var bid = personRegister.FetchBuildingManagerBuildingId(userId);
            var apt = buildingRegister.CreateApartment(level, number, tenantsAllowed, facingDirection, bid);
            return apt;
        }

        public IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var apt = buildingRegister.UpdateApartment(aptId, buildingId, level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        public IApartment FetchApartment(int apartmentId, string userId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var apt = buildingRegister.FetchApartment(buildingId, apartmentId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(string userId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var apt = buildingRegister.FetchApartments(buildingId);
            return apt;
        }

        #endregion 

        #region Facility

        public IFacility CreateFacility(string userId, string Level, string Number)
        {
            int buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var facility = buildingRegister.CreateFacility(buildingId, Level, Number);
            return facility;
        }

        public IFacility UpdateFacility(string userId, int FacilityId, string Level, string Number)
        {
            int buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var facility = buildingRegister.UpdateFacility(buildingId, FacilityId, Level, Number);
            return facility;
        }

        public IFacility FetchFacility(string userId, int FacilityId)
        {
            int buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var facility = buildingRegister.FetchFacility(buildingId, FacilityId);
            return facility;
        }

        #endregion

        #region Booking
        
        public IBooking CreateBooking(string userId, int FacilityId, DateTime StartTime, DateTime EndTime)
        {
            var bm = personRegister.FetchBuildingManager(userId);
            var booking = buildingRegister.CreateBooking(bm.BuildingId, FacilityId, bm.Id, StartTime, EndTime);
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(string userId, int FacilityId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var bookings = buildingRegister.FetchFacilityBookings(buildingId, FacilityId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int FacilityId, int BookingId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var bookings = buildingRegister.FetchBooking(buildingId, FacilityId, BookingId);
            return bookings;
        }

        public IBooking CancelBooking(string userId, int FacilityId, int BookingId)
        {
            var buildingId = personRegister.FetchBuildingManagerBuildingId(userId);
            var booking = buildingRegister.DeleteBooking(buildingId, FacilityId, BookingId);
            return booking;
        }

        public IEnumerable<IBooking> FetchPersonBookings(string userId)
        {
            var bookings = personRegister.FetchPersonBookings(userId);
            return bookings;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId)
        {
            var tenant = personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId);
            return tenant;
        }

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

        public ITenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = personRegister.ChangeApartment(userId, apartmentId);
            return tenant;
        }

        public IEnumerable<ITenant> FetchBuildingTenants(string userId)
        {
            var tenants = personRegister.FetchBuildingTenants(personRegister.FetchBuildingManagerBuildingId(userId));
            return tenants;
        }

        #endregion

    }
}
