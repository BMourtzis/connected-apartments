using System;
using System.Collections.Generic;
using ConnApsDomain.Models;
using ConnApsDomain.Registers;

namespace ConnApsDomain
{
    /// <summary>
    /// The Facade of the ConnApsDomain.
    /// All the needed methods are exposed here.
    /// </summary>
    public class Facade: IDisposable, IDisposableFacade, IFacade
    {
        /// <summary>
        /// The Register that communicates with the Building Class
        /// </summary>
        private readonly BuildingRegister _buildingRegister;
        /// <summary>
        /// The Register that communicates with the Person Class
        /// </summary>
        private readonly PersonRegister _personRegister;

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Facade()
        {
            _buildingRegister = new BuildingRegister();
            _personRegister = new PersonRegister();
        }

        //public Facade(IConnApsContext context)
        //{

        //}

        #endregion

        #region Person

        /// <summary>
        /// Updates the Person details
        /// </summary>
        /// <param name="firstName">The new First Name of the Person</param>
        /// <param name="lastName">The new Last Name of the Person</param>
        /// <param name="dob">The new Date of Birth of the Person</param>
        /// <param name="phone">The new Phone of the Person</param>
        /// <param name="userId">The Id of the user connected to the Person. Used to get the person</param>
        public void UpdatePerson(string firstName, string lastName, DateTime dob, string phone, string userId)
        {
            _personRegister.UpdatePerson(firstName, lastName,dob, phone, userId);
        }

        #endregion

        #region Apartment

        /// <summary>
        /// Creates a new Apartment instance
        /// </summary>
        /// <param name="level">The Level the apartment is on</param>
        /// <param name="number">The Number of the apartment</param>
        /// <param name="tenantsAllowed">The number of tenants allowed in the apartment</param>
        /// <param name="facingDirection">The Direction the apartment is facing</param>
        /// <param name="userId">The Id of the User of the building manager that creates the apartment</param>
        /// <returns>Returns an interface of the newly created Apartment</returns>
        public IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var apt = _buildingRegister.CreateApartment(level, number, tenantsAllowed, facingDirection, _personRegister.FetchBuildingId(userId));
            return apt;
        }

        /// <summary>
        /// Updates the apartment details
        /// </summary>
        /// <param name="aptId">The Id of the apartment</param>
        /// <param name="level">The new level of the apartment</param>
        /// <param name="number">The new number of the apartment</param>
        /// <param name="tenantsAllowed">The new number of tenants allowed</param>
        /// <param name="facingDirection">The new direction that the partment is facing</param>
        /// <param name="userId">The Id of the user (building manager) that is making the update</param>
        /// <returns>Returns the interface of the newly updated apartment</returns>
        public IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection, string userId)
        {
            var apt = _buildingRegister.UpdateApartment(aptId, _personRegister.FetchBuildingId(userId), level, number, tenantsAllowed, facingDirection);
            return apt;
        }

        /// <summary>
        /// Fetches an apartment
        /// </summary>
        /// <param name="apartmentId">The Id of the apartment</param>
        /// <param name="userId">The Id of the user fetching the apartment</param>
        /// <returns>Returns an Interface of the apartment</returns>
        public IApartment FetchApartment(int apartmentId, string userId)
        {
            var apt = _buildingRegister.FetchApartment(_personRegister.FetchBuildingId(userId), apartmentId);
            return apt;
        }

        /// <summary>
        /// Fetchs the apartment the user is lives in
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Returns the Interface of the apartment</returns>
        public IApartment FetchApartment(string userId)
        {
            var apt = _personRegister.FetchApartment(userId);
            return apt;
        }

        /// <summary>
        /// Fetches all the apartments from a building
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Returns a List of Interfaces of the apartment</returns>
        public IEnumerable<IApartment> FetchApartments(string userId)
        {
            var apt = _buildingRegister.FetchApartments(_personRegister.FetchBuildingId(userId));
            return apt;
        }

        #endregion

        #region Booking

        /// <summary>
        /// Creates a new Booking
        /// </summary>
        /// <param name="userId">The Id of the user that creates the booking</param>
        /// <param name="facilityId">The Id of the facility that the booking is for</param>
        /// <param name="startTime">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the booking ends</param>
        /// <returns>Returns an Interface of the newly created Booking</returns>
        public IBooking CreateBooking(string userId, int facilityId, DateTime startTime, DateTime endTime)
        {
            var bm = _personRegister.FetchBuildingManager(userId);
            var booking = _buildingRegister.CreateBooking((int)bm.BuildingId, facilityId, bm.Id, startTime, endTime);
            return booking;
        }

        /// <summary>
        /// Fetches the bookings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="facilityId"></param>
        /// <returns>Returns a list of Interfaces of Bookings</returns>
        public IEnumerable<IBooking> FetchBookings(string userId, int facilityId)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var bookings = _buildingRegister.FetchBookings(buildingId, facilityId);
            return bookings;
        }

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns an Interface of the Booking</returns>
        public IBooking FetchBooking(string userId, int bookingId)
        {
            var bookings = _personRegister.FetchBooking(userId, bookingId);
            return bookings;
        }

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the facility the booking is at</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns an Interface of the Booking</returns>
        public IBooking FetchBooking(string userId, int facilityId, int bookingId)
        {
            var bookings = _buildingRegister.FetchBooking(_personRegister.FetchBuildingId(userId), facilityId, bookingId);
            return bookings;
        }

        /// <summary>
        /// Fetches all the bookings from a person
        /// </summary>
        /// <param name="userId">The id of the person</param>
        /// <returns>Returns a List of Interfaces of Booking</returns>
        public IEnumerable<IBooking> FetchBookings(string userId)
        {
            var bookings = _personRegister.FetchBookings(userId);
            return bookings;
        }

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="bookingId">The Id of the booking</param>
        public void CancelBooking(string userId, int bookingId)
        {
            _personRegister.CancelBooking(userId, bookingId);
        }

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the facility the booking is in</param>
        /// <param name="bookingId">The Id of the booking</param>
        public void CancelBooking(string userId, int facilityId, int bookingId)
        {
            _buildingRegister.CancelBooking(_personRegister.FetchBuildingId(userId), facilityId, bookingId);
        }

        #endregion

        #region Building

        /// <summary>
        /// Creates a new Building, as well as a Building Manager
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The Date of Birth of the Building Manager</param>
        /// <param name="phone">The Phone of the Building Manager</param>
        /// <param name="userId">The Id of the User that is going to connecte to the Building Manager</param>
        /// <param name="buildingName">The Name of the new Building</param>
        /// <param name="address">The Address of the new Building</param>
        /// <returns>Returns an Interface of the newly created Building</returns>
        public IBuilding CreateBuilding(string firstName, string lastName, DateTime dob, string phone, string userId, string buildingName, string address)
        {
            var building = _buildingRegister.CreateBuilding(buildingName, address);
            var bm = _personRegister.CreateBuildingManager(firstName, lastName, dob, address, userId, building.Id);
            return building;
        }

        /// <summary>
        /// Updates the Building Details
        /// </summary>
        /// <param name="userId">The Id of the user updating the building</param>
        /// <param name="buildingName">The new Name of the Building</param>
        /// <param name="address">The new Address of the Building</param>
        /// <returns>Returns an Interface of the newly updated Building</returns>
        public IBuilding UpdateBuilding(string userId, string buildingName, string address)
        {
            var building = _buildingRegister.UpdateBuilding(_personRegister.FetchBuildingId(userId), buildingName, address);
            return building;
        }

        /// <summary>
        /// Fetches a building
        /// </summary>
        /// <param name="id">The Id of the building</param>
        /// <returns>Returns an Interface of the Building</returns>
        public IBuilding FetchBuilding(int id)
        {
            var building = _buildingRegister.FetchBuilding(id);
            return building;
        }

        /// <summary>
        /// Fetches a Building
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <returns>Returns a Interface of the Building</returns>
        public IBuilding FetchBuilding(string id)
        {
            var building = _buildingRegister.FetchBuilding(_personRegister.FetchPerson(id).BuildingId);
            return building;
        }

        #endregion

        #region Facility

        /// <summary>
        /// Creates a new facility
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="level">The level of the facility</param>
        /// <param name="number">The number of the facility</param>
        /// <returns>Returns an Interface of the newly created Facility</returns>
        public IFacility CreateFacility(string userId, string level, string number)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.CreateFacility(buildingId, level, number);
            return facility;
        }

        /// <summary>
        /// Updates a Facility's details
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the Facility</param>
        /// <param name="level">The new level of the facility</param>
        /// <param name="number"></param>
        /// <returns>Returns the Interface the newly updated Facility</returns>
        public IFacility UpdateFacility(string userId, int facilityId, string level, string number)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.UpdateFacility(buildingId, facilityId, level, number);
            return facility;
        }

        /// <summary>
        /// Fetches a Facility
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="facilityId">The Id of the Facility</param>
        /// <returns>Returns the Interface of the facility</returns>
        public IFacility FetchFacility(string userId, int facilityId)
        {
            var buildingId = _personRegister.FetchBuildingId(userId);
            var facility = _buildingRegister.FetchFacility(buildingId, facilityId);
            return facility;
        }

        /// <summary>
        /// Fetches all the Facility of a building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns a List of Interfaces of the Facilities</returns>
        public IEnumerable<IFacility> FetchFacilities(string id)
        {
            var buildingId = _personRegister.FetchBuildingId(id);
            var facilities = _buildingRegister.FetchFacilities(buildingId);
            return facilities;
        }

        #endregion

        #region Manager

        /// <summary>
        /// Creates a new Building Manager
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The Date of Birth of the Building Manager</param>
        /// <param name="phone">The Phone of  the Building Manager</param>
        /// <param name="userId">The Id of the User that the Building Manager connects to</param>
        /// <param name="buildingId">The Id of the Building</param>
        /// <returns>Returns the Interface of the newly created Building Manager</returns>
        public IBuildingManager CreateBuildingManager(string firstName, string lastName, DateTime dob, string phone, string userId, int buildingId)
        {
            var manager = _personRegister.CreateBuildingManager(firstName, lastName, dob, phone, userId, buildingId);
            return manager;
        }

        /// <summary>
        /// Fetch Building Manager
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns an Interface of the Building Manager</returns>
        public IBuildingManager FetchBuildingManager(string id)
        {
            var manager = _personRegister.FetchBuildingManager(id);
            return manager;
        }

        /// <summary>
        /// Fetchs all the Building Managers of the Building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns a List of Interfaces of the Building Managers</returns>
        public IEnumerable<IBuildingManager> FetchBuildingManagers(string id)
        {
            var managers = _buildingRegister.FetchBuildingManagers(_personRegister.FetchBuildingId(id));
            return managers;
        }

        /// <summary>
        /// Updates Building Manager details
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="firstName">The new First Name of the Building Manager</param>
        /// <param name="lastName">The new Last Name of the Building Manager</param>
        /// <param name="dob">The new Date of Birth of the Building Manager</param>
        /// <param name="phone">The new Phone of the Building Manager</param>
        /// <returns>Returns an Interface of the Building Manager</returns>
        public IBuildingManager UpdateBuildingManager(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var manager = _personRegister.UpdateBuildingManager(userId, firstName, lastName, dob, phone);
            return manager;
        }

        #endregion

        #region Tenant

        /// <summary>
        /// Creates a new Tenant
        /// </summary>
        /// <param name="firstName">The First Name of the Tenant</param>
        /// <param name="lastName">The Last Name of the Tenant</param>
        /// <param name="dob">The Date of Birth of the Tenant</param>
        /// <param name="phone">The Phone of the Tenant</param>
        /// <param name="userId">The Id of the user that connects to the Tenant</param>
        /// <param name="apartmentId">The Id of the apartment that the Tenant will live in</param>
        /// <param name="creatorId">The Id of the building manager that creates the tenant</param>
        /// <returns>Returns an Interface of the newly created Tenant</returns>
        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, string creatorId)
        {
            var tenant = _personRegister.CreateTenant(firstName, lastName, dob, phone, userId, apartmentId, _personRegister.FetchBuildingId(creatorId));
            return tenant;
        }

        /// <summary>
        /// Fetches a Tenant
        /// </summary>
        /// <param name="id">The id of the User</param>
        /// <returns>Returns an Interface of the Tenant</returns>
        public ITenant FetchTenant(string id)
        {
            var tenant = _personRegister.FetchTenant(id);
            return tenant;
        }

        /// <summary>
        /// Fetches all the Tenant of a building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns an List of Interfaces of Tenants</returns>
        public IEnumerable<ITenant> FetchTenants(string id)
        {
            var tenants = _personRegister.FetchTenants(_personRegister.FetchBuildingId(id));
            return tenants;
        }

        /// <summary>
        /// Updatees Tenant Details
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="firstName">The new First Name of the Tenant</param>
        /// <param name="lastName">The new Last name of the Tenant</param>
        /// <param name="dob">The new Date of Birth of the Tenant</param>
        /// <param name="phone">The new Phone of the Tenant</param>
        /// <returns>Returns an Interface of Teannts</returns>
        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = _personRegister.UpdateTenant(userId, firstName, lastName, dob, phone);
            return tenant;
        }
        
        //TODO: Add verification that the one doing the change is part of the building
        /// <summary>
            /// Chnages apartments for a tenant
            /// </summary>
            /// <param name="userId">The Id of the User that connects to the Tenant</param>
            /// <param name="apartmentId">The Id of the new Apartment</param>
            /// <returns>Returns the Interface the newly updated Tenant</returns>
        public ITenant ChangeApartment(string userId, int apartmentId)
            {
                var tenant = _personRegister.ChangeApartment(userId, apartmentId);
                return tenant;
            }

        #endregion

        /// <summary>
        /// Disposes the class, including its properties
        /// </summary>
        public void Dispose()
        {
            _buildingRegister.Dispose();
            _personRegister.Dispose();
        }
    }

    public interface IFacade
    {
        #region Person

        /// <summary>
        /// Updates the Person details
        /// </summary>
        /// <param name="firstName">The new First Name of the Person</param>
        /// <param name="lastName">The new Last Name of the Person</param>
        /// <param name="dob">The new Date of Birth of the Person</param>
        /// <param name="phone">The new Phone of the Person</param>
        /// <param name="userId">The Id of the user connected to the Person. Used to get the person</param>
        void UpdatePerson(string firstName, string lastName, DateTime dob, string phone, string userId);

        #endregion

        #region Apartment

        /// <summary>
        /// Creates a new Apartment instance
        /// </summary>
        /// <param name="level">The Level the apartment is on</param>
        /// <param name="number">The Number of the apartment</param>
        /// <param name="tenantsAllowed">The number of tenants allowed in the apartment</param>
        /// <param name="facingDirection">The Direction the apartment is facing</param>
        /// <param name="userId">The Id of the User of the building manager that creates the apartment</param>
        /// <returns>Returns an interface of the newly created Apartment</returns>
        IApartment CreateApartment(string level, string number, int tenantsAllowed, string facingDirection, string userId);

        /// <summary>
        /// Updates the apartment details
        /// </summary>
        /// <param name="aptId">The Id of the apartment</param>
        /// <param name="level">The new level of the apartment</param>
        /// <param name="number">The new number of the apartment</param>
        /// <param name="tenantsAllowed">The new number of tenants allowed</param>
        /// <param name="facingDirection">The new direction that the partment is facing</param>
        /// <param name="userId">The Id of the user (building manager) that is making the update</param>
        /// <returns>Returns the interface of the newly updated apartment</returns>
        IApartment UpdateApartment(int aptId, string level, string number, int tenantsAllowed, string facingDirection, string userId);

        /// <summary>
        /// Fetches an apartment
        /// </summary>
        /// <param name="apartmentId">The Id of the apartment</param>
        /// <param name="userId">The Id of the user fetching the apartment</param>
        /// <returns>Returns an Interface of the apartment</returns>
        IApartment FetchApartment(int apartmentId, string userId);

        /// <summary>
        /// Fetchs the apartment the user is lives in
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Returns the Interface of the apartment</returns>
        IApartment FetchApartment(string userId);

        /// <summary>
        /// Fetches all the apartments from a building
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Returns a List of Interfaces of the apartment</returns>
        IEnumerable<IApartment> FetchApartments(string userId);

        #endregion

        #region Booking

        /// <summary>
        /// Creates a new Booking
        /// </summary>
        /// <param name="userId">The Id of the user that creates the booking</param>
        /// <param name="facilityId">The Id of the facility that the booking is for</param>
        /// <param name="startTime">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the booking ends</param>
        /// <returns>Returns an Interface of the newly created Booking</returns>
        IBooking CreateBooking(string userId, int facilityId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Fetches the bookings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="facilityId"></param>
        /// <returns>Returns a list of Interfaces of Bookings</returns>
        IEnumerable<IBooking> FetchBookings(string userId, int facilityId);

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns an Interface of the Booking</returns>
        IBooking FetchBooking(string userId, int bookingId);

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the facility the booking is at</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns an Interface of the Booking</returns>
        IBooking FetchBooking(string userId, int facilityId, int bookingId);

        /// <summary>
        /// Fetches all the bookings from a person
        /// </summary>
        /// <param name="userId">The id of the person</param>
        /// <returns>Returns a List of Interfaces of Booking</returns>
        IEnumerable<IBooking> FetchBookings(string userId);

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="bookingId">The Id of the booking</param>
        void CancelBooking(string userId, int bookingId);

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the facility the booking is in</param>
        /// <param name="bookingId">The Id of the booking</param>
        void CancelBooking(string userId, int facilityId, int bookingId);

        #endregion

        #region Building

        /// <summary>
        /// Creates a new Building, as well as a Building Manager
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The Date of Birth of the Building Manager</param>
        /// <param name="phone">The Phone of the Building Manager</param>
        /// <param name="userId">The Id of the User that is going to connecte to the Building Manager</param>
        /// <param name="buildingName">The Name of the new Building</param>
        /// <param name="address">The Address of the new Building</param>
        /// <returns>Returns an Interface of the newly created Building</returns>
        IBuilding CreateBuilding(string firstName, string lastName, DateTime dob, string phone, string userId, string buildingName, string address);

        /// <summary>
        /// Updates the Building Details
        /// </summary>
        /// <param name="userId">The Id of the user updating the building</param>
        /// <param name="buildingName">The new Name of the Building</param>
        /// <param name="address">The new Address of the Building</param>
        /// <returns>Returns an Interface of the newly updated Building</returns>
        IBuilding UpdateBuilding(string userId, string buildingName, string address);

        /// <summary>
        /// Fetches a building
        /// </summary>
        /// <param name="id">The Id of the building</param>
        /// <returns>Returns an Interface of the Building</returns>
        IBuilding FetchBuilding(int id);

        /// <summary>
        /// Fetches a Building
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <returns>Returns a Interface of the Building</returns>
        IBuilding FetchBuilding(string id);

        #endregion

        #region Facility

        /// <summary>
        /// Creates a new facility
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="level">The level of the facility</param>
        /// <param name="number">The number of the facility</param>
        /// <returns>Returns an Interface of the newly created Facility</returns>
        IFacility CreateFacility(string userId, string level, string number);

        /// <summary>
        /// Updates a Facility's details
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="facilityId">The Id of the Facility</param>
        /// <param name="level">The new level of the facility</param>
        /// <param name="number"></param>
        /// <returns>Returns the Interface the newly updated Facility</returns>
        IFacility UpdateFacility(string userId, int facilityId, string level, string number);

        /// <summary>
        /// Fetches a Facility
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="facilityId">The Id of the Facility</param>
        /// <returns>Returns the Interface of the facility</returns>
        IFacility FetchFacility(string userId, int facilityId);

        /// <summary>
        /// Fetches all the Facility of a building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns a List of Interfaces of the Facilities</returns>
        IEnumerable<IFacility> FetchFacilities(string id);

        #endregion

        #region Building Manager

        /// <summary>
        /// Creates a new Building Manager
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The Date of Birth of the Building Manager</param>
        /// <param name="phone">The Phone of  the Building Manager</param>
        /// <param name="userId">The Id of the User that the Building Manager connects to</param>
        /// <param name="buildingId">The Id of the Building</param>
        /// <returns>Returns the Interface of the newly created Building Manager</returns>
        IBuildingManager CreateBuildingManager(string firstName, string lastName, DateTime dob, string phone, string userId, int buildingId);

        /// <summary>
        /// Fetch Building Manager
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns an Interface of the Building Manager</returns>
        IBuildingManager FetchBuildingManager(string id);

        /// <summary>
        /// Fetchs all the Building Managers of the Building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns a List of Interfaces of the Building Managers</returns>
        IEnumerable<IBuildingManager> FetchBuildingManagers(string id);

        /// <summary>
        /// Updates Building Manager details
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="firstName">The new First Name of the Building Manager</param>
        /// <param name="lastName">The new Last Name of the Building Manager</param>
        /// <param name="dob">The new Date of Birth of the Building Manager</param>
        /// <param name="phone">The new Phone of the Building Manager</param>
        /// <returns>Returns an Interface of the Building Manager</returns>
        IBuildingManager UpdateBuildingManager(string userId, string firstName, string lastName, DateTime dob, string phone);

        #endregion

        #region Tenant

        /// <summary>
        /// Creates a new Tenant
        /// </summary>
        /// <param name="firstName">The First Name of the Tenant</param>
        /// <param name="lastName">The Last Name of the Tenant</param>
        /// <param name="dob">The Date of Birth of the Tenant</param>
        /// <param name="phone">The Phone of the Tenant</param>
        /// <param name="userId">The Id of the user that connects to the Tenant</param>
        /// <param name="apartmentId">The Id of the apartment that the Tenant will live in</param>
        /// <param name="creatorId">The Id of the building manager that creates the tenant</param>
        /// <returns>Returns an Interface of the newly created Tenant</returns>
        ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, string creatorId);

        /// <summary>
        /// Fetches a Tenant
        /// </summary>
        /// <param name="id">The id of the User</param>
        /// <returns>Returns an Interface of the Tenant</returns>
        ITenant FetchTenant(string id);

        /// <summary>
        /// Fetches all the Tenant of a building
        /// </summary>
        /// <param name="id">The Id of the User</param>
        /// <returns>Returns an List of Interfaces of Tenants</returns>
        IEnumerable<ITenant> FetchTenants(string id);

        /// <summary>
        /// Updatees Tenant Details
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="firstName">The new First Name of the Tenant</param>
        /// <param name="lastName">The new Last name of the Tenant</param>
        /// <param name="dob">The new Date of Birth of the Tenant</param>
        /// <param name="phone">The new Phone of the Tenant</param>
        /// <returns>Returns an Interface of Teannts</returns>
        ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone);

        /// <summary>
        /// Chnages apartments for a tenant
        /// </summary>
        /// <param name="userId">The Id of the User that connects to the Tenant</param>
        /// <param name="apartmentId">The Id of the new Apartment</param>
        /// <returns>Returns the Interface the newly updated Tenant</returns>
        ITenant ChangeApartment(string userId, int apartmentId);

        #endregion

        /// <summary>
        /// Disposes the class, including its properties
        /// </summary>
        void Dispose();
    }
}
