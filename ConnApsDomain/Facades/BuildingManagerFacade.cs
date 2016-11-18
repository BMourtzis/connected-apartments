using System;
using System.Collections.Generic;
using ConnApsDomain.Models;

namespace ConnApsDomain.Facades
{
    public class BuildingManagerFacade: Facade
    {

        #region Constructors

        public BuildingManagerFacade(): base() {}

        #endregion

        #region BuildingManager

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var bm = PersonRegister.FetchBuildingManager(userId);
            return bm;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var bm = PersonRegister.UpdateBuildingManager(userId, firstname, lastname, dateofbirth, newPhone);
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
            var building = BuildingRegister.UpdateBuilding(PersonRegister.FetchBuildingManagerBuildingId(userId), buildingName, address);
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = BuildingRegister.FetchBuilding(buildingId);
            return building;
        }

        public IBuilding GetTenantBuilding(string userId)
        {
            var building = BuildingRegister.FetchBuilding(PersonRegister.FetchTenant(userId).BuildingId);
            return building;
        }

        public IBuilding FetchBuildingManagerBuilding(string userId)
        {
            var building = BuildingRegister.FetchBuilding(PersonRegister.FetchBuildingManagerBuildingId(userId));
            return building;
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var bid = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var apt = BuildingRegister.CreateApartment(level, number, tenantsAllowed, facingDirection, bid);
            return apt;
        }

        public IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var apt = BuildingRegister.UpdateApartment(aptId, buildingId, level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        public IApartment FetchApartment(int apartmentId, string userId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var apt = BuildingRegister.FetchApartment(buildingId, apartmentId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(string userId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var apt = BuildingRegister.FetchApartments(buildingId);
            return apt;
        }

        #endregion 

        #region Facility

        public IFacility CreateFacility(string userId, string level, string number)
        {
            int buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var facility = BuildingRegister.CreateFacility(buildingId, level, number);
            return facility;
        }

        public IFacility UpdateFacility(string userId, int facilityId, string level, string number)
        {
            int buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var facility = BuildingRegister.UpdateFacility(buildingId, facilityId, level, number);
            return facility;
        }

        public IFacility FetchFacility(string userId, int facilityId)
        {
            int buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var facility = BuildingRegister.FetchFacility(buildingId, facilityId);
            return facility;
        }

        public IEnumerable<IFacility> FetchFacilities(string userId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var facilities = BuildingRegister.FetchFacilities(buildingId);
            return facilities;
        }

        #endregion

        #region Booking

        public IBooking CreateBooking(string userId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var bm = PersonRegister.FetchBuildingManager(userId);
            var booking = BuildingRegister.CreateBooking(bm.BuildingId, facilityId, bm.Id, startTime, endTime);
            return booking;
        }

        public IEnumerable<IBooking> FetchFacilityBookings(string userId, int facilityId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var bookings = BuildingRegister.FetchFacilityBookings(buildingId, facilityId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int facilityId, int bookingId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var bookings = BuildingRegister.FetchBooking(buildingId, facilityId, bookingId);
            return bookings;
        }

        public IBooking CancelBooking(string userId, int facilityId, int bookingId)
        {
            var buildingId = PersonRegister.FetchBuildingManagerBuildingId(userId);
            var booking = BuildingRegister.DeleteBooking(buildingId, facilityId, bookingId);
            return booking;
        }

        public IEnumerable<IBooking> FetchPersonBookings(string userId)
        {
            var bookings = PersonRegister.FetchPersonBookings(userId);
            return bookings;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, string managerId)
        {
            int buildingId = GetBuildingId(managerId);
            var tenant = PersonRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId, buildingId);
            return tenant;
        }

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

        public ITenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = PersonRegister.ChangeApartment(userId, apartmentId);
            return tenant;
        }

        public IEnumerable<ITenant> FetchBuildingTenants(string userId)
        {
            var tenants = PersonRegister.FetchBuildingTenants(PersonRegister.FetchBuildingManagerBuildingId(userId));
            return tenants;
        }

        #endregion
    }
}
