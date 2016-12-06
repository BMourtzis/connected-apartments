using System;
using System.Collections.Generic;
using ConnApsDomain.Models;
using ConnApsDomain.Registers;

namespace ConnApsDomain
{
    public class Facade: IDisposable, IDisposableFacade
    {
        private readonly BuildingRegister _buildingRegister;
        private readonly PersonRegister _personRegister;

        #region Constructors

        public Facade()
        {
            _buildingRegister = new BuildingRegister();
            _personRegister = new PersonRegister();
        }

        #endregion


        #region Person

        public void UpdatePerson(string firstname, string lastname, DateTime dateofbirth, string phone, string userId)
        {
            _personRegister.UpdatePerson(firstname, lastname,dateofbirth, phone, userId);
        }

        #endregion

        #region Apartment

        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var apt = _buildingRegister.CreateApartment(level, number, tenantsAllowed, facingDirection, _personRegister.FetchBuildingId(userId));
            return apt;
        }

        public IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var apt = _buildingRegister.UpdateApartment(aptId, _personRegister.FetchBuildingId(userId), level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        public IApartment FetchApartment(int apartmentId, string userId)
        {
            var apt = _buildingRegister.FetchApartment(_personRegister.FetchBuildingId(userId), apartmentId);
            return apt;
        }

        public IApartment FetchApartment(string userId)
        {
            var apt = _personRegister.FetchApartment(userId);
            return apt;
        }

        public IEnumerable<IApartment> FetchApartments(string userId)
        {
            var apt = _buildingRegister.FetchApartments(_personRegister.FetchBuildingId(userId));
            return apt;
        }

        #endregion

        #region Booking

        public IBooking CreateBooking(string userId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var bm = _personRegister.FetchBuildingManager(userId);
            var booking = _buildingRegister.CreateBooking((int)bm.BuildingId, facilityId, bm.Id, startTime, endTime);
            return booking;
        }

        public IEnumerable<IBooking> FetchBookings(string userId, int facilityId)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var bookings = _buildingRegister.FetchBookings(buildingId, facilityId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int bookingId)
        {
            var bookings = _personRegister.FetchBooking(userId, bookingId);
            return bookings;
        }

        public IBooking FetchBooking(string userId, int facilityId, int bookingId)
        {
            var bookings = _buildingRegister.FetchBooking(_personRegister.FetchBuildingId(userId), facilityId, bookingId);
            return bookings;
        }

        public IEnumerable<IBooking> FetchBookings(string userId)
        {
            var bookings = _personRegister.FetchBookings(userId);
            return bookings;
        }

        public void CancelBooking(string userId, int bookingId)
        {
            _personRegister.CancelBooking(userId, bookingId);
        }

        public void CancelBooking(string userId, int facilityId, int bookingId)
        {
            _buildingRegister.CancelBooking(_personRegister.FetchBuildingId(userId), facilityId, bookingId);
        }

        #endregion

        #region Building

        public IBuilding CreateBuilding(string firstname, string lastname, DateTime dateofbirth, string phone, string userid, string buildingname, string address)
        {
            var building = _buildingRegister.CreateBuilding(buildingname, address);
            var bm = _personRegister.CreateBuildingManager(firstname, lastname, dateofbirth, address, userid, building.Id);
            return building;
        }

        public IBuilding UpdateBuilding(string userId, string buildingName, string address)
        {
            var building = _buildingRegister.UpdateBuilding(_personRegister.FetchBuildingId(userId), buildingName, address);
            return building;
        }

        public IBuilding FetchBuilding(int buildingId)
        {
            var building = _buildingRegister.FetchBuilding(buildingId);
            return building;
        }

        public IBuilding FetchBuilding(string userId)
        {
            var building = _buildingRegister.FetchBuilding(_personRegister.FetchBuildingId(userId));
            return building;
        }

        #endregion

        #region Facility

        public IFacility CreateFacility(string userId, string level, string number)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.CreateFacility(buildingId, level, number);
            return facility;
        }

        public IFacility UpdateFacility(string userId, int facilityId, string level, string number)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.UpdateFacility(buildingId, facilityId, level, number);
            return facility;
        }

        public IFacility FetchFacility(string userId, int facilityId)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.FetchFacility(buildingId, facilityId);
            return facility;
        }

        public IEnumerable<IFacility> FetchFacilities(string userId)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facilities = _buildingRegister.FetchFacilities(buildingId);
            return facilities;
        }

        #endregion

        #region Manager

        public IBuildingManager CreateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string phone, string userid, int buildingId)
        {
            var manager = _personRegister.CreateBuildingManager(firstname, lastname, dateofbirth, phone, userid, buildingId);
            return manager;
        }

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var manager = _personRegister.FetchBuildingManager(userId);
            return manager;
        }

        public IEnumerable<IBuildingManager> FetchBuildingManagers(string userId)
        {
            var managers = _buildingRegister.FetchBuildingManagers(_personRegister.FetchBuildingId(userId));
            return managers;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var manager = _personRegister.UpdateBuildingManager(userId, firstname, lastname, dateofbirth, newPhone);
            return manager;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, string creatorId)
        {
            var tenant = _personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId, _personRegister.FetchBuildingId(creatorId));
            return tenant;
        }

        public ITenant FetchTenant(string userId)
        {
            var tenant = _personRegister.FetchTenant(userId);
            return tenant;
        }

        public IEnumerable<ITenant> FetchTenants(string userId)
        {
            var tenants = _personRegister.FetchTenants(_personRegister.FetchBuildingId(userId));
            return tenants;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = _personRegister.UpdateTenant(userId, firstName, lastName, dob, phone);
            return tenant;
        }

        public ITenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = _personRegister.ChangeApartment(userId, apartmentId);
            return tenant;
        }

        #endregion

        public void Dispose()
        {
            _buildingRegister.Dispose();
            _personRegister.Dispose();
        }
    }
}
