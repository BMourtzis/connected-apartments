using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;

namespace ConnApsDomain.Registers
{
    /// <summary>
    /// Connects to the Person via the DbContext
    /// </summary>
    internal class PersonRegister: IDisposable
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// Initialises a new ConnApsContext
        /// </summary>
        public PersonRegister()
        {
            _context = new ConnApsContext();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Connects to the Database
        /// </summary>
        private readonly ConnApsContext _context;

        #endregion

        #region Person

        /// <summary>
        /// Fetches the Building Id from a person
        /// </summary>
        /// <param name="userId">The Id of the User connected to a person</param>
        /// <returns>Returns the Building Id</returns>
        public int FetchBuildingId(string userId)
        {
            var person = _context.People.FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person.BuildingId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Person FetchPerson(string userId)
        {
            var person = _context.People.FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person;
        }

        /// <summary>
        /// Updates information of a person
        /// </summary>
        /// <param name="firstName">The new first name of the person</param>
        /// <param name="lastName">The new last name of the person</param>
        /// <param name="dob">The new Date of Birth of the person</param>
        /// <param name="phone">The new Phone of the person</param>
        /// <param name="userId">The Id of the user connected to person. Used to find the person.</param>
        public void UpdatePerson(string firstName, string lastName, DateTime dob, string phone, string userId)
        {
            var person = FetchPerson(userId);
            person.UpdatePerson(firstName, lastName, dob, phone);
        }

        #endregion

        #region BuildingManager

        /// <summary>
        /// Creates a new Building Manager
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The date of birth of the Building Manager</param>
        /// <param name="phone">The phone of the Building Manager</param>
        /// <param name="userId">The Id of the user that it connects to</param>
        /// <param name="buildingId">The Id of the building that the manager is a part of</param>
        /// <returns>Returns the newly created Building Manager</returns>
        public BuildingManager CreateBuildingManager(string firstName, string lastName, DateTime dob, string phone, string userId, int buildingId)
        {
            var bm = new BuildingManager(firstName, lastName, dob, phone, userId, buildingId);
            _context.BuildingManagers.Add(bm);
            _context.SaveChanges();
            return bm;
        }

        /// <summary>
        /// Updates information of a Building Manager
        /// </summary>
        /// <param name="userId">The Id of the user the building manager is connected to</param>
        /// <param name="firstName">The new First Name of the building manager</param>
        /// <param name="lastName">The new Last name of the building manager</param>
        /// <param name="dob">The new Date of Birth of the building manager</param>
        /// <param name="phone">The new phone of the Building Manager</param>
        /// <returns>Returns the new updated Building Manager</returns>
        public BuildingManager UpdateBuildingManager(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var bm = FetchBuildingManager(userId);
            bm.UpdatePerson(firstName, lastName, dob, phone);
            _context.SaveChanges();
            return bm;
        }

        /// <summary>
        /// Fetches a building manager
        /// </summary>
        /// <param name="userId">The Id of the user the Building manager is connected to</param>
        /// <returns>Returns the building manager with the same user Id</returns>
        public BuildingManager FetchBuildingManager(string userId)
        {
            //TODO: Check if all the BuildingManager Information is loaded.
            var bm = FetchPerson(userId) as BuildingManager;

            if (bm == null)
            {
                throw new InternalException();
            }

            return bm;
        }

        /// <summary>
        /// Fetchs all the building managers from the database
        /// </summary>
        /// <returns>A list of building managers</returns>
        public IEnumerable<BuildingManager> FetchBuildingManagers()
        {
            var bms = _context.BuildingManagers;
            return bms;
        }

        #endregion

        #region Tenant
        
        /// <summary>
        /// Creates a new Tenant
        /// </summary>
        /// <param name="firstName">The First Name of the Tenant</param>
        /// <param name="lastName">The Last Name of the Tenant</param>
        /// <param name="dob">The Date of Birth of the Tenant</param>
        /// <param name="phone">The phone of the Tenant</param>
        /// <param name="userId">The Id of the User the connect will connect to </param>
        /// <param name="apartmentId">The Id of the apartment the tenant will live in</param>
        /// <param name="buildingId">The id the building the tenant will be part of</param>
        /// <returns>Returns the newly created tenant</returns>
        public Tenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, int buildingId)
        {
            var tenant = new Tenant(firstName, lastName, dob, phone, userId, apartmentId, buildingId);
            _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return tenant;
        }

        /// <summary>
        /// Updates Tenant details
        /// </summary>
        /// <param name="userId">The Id of the user the tenant is connected to</param>
        /// <param name="firstName">The new First Name of the Tenant</param>
        /// <param name="lastName">The new Last Name of the Tenant</param>
        /// <param name="dob">The new Date of Birth of the Tenant</param>
        /// <param name="phone">The new Phone of the tenant</param>
        /// <returns>Returns the newly updated tenant</returns>
        public Tenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = FetchTenant(userId);
            tenant.UpdatePerson(firstName, lastName, dob, phone);
            _context.SaveChanges();
            return tenant;
        }

        /// <summary>
        /// Fetches a tenant with the Id supplied
        /// </summary>
        /// <param name="userId">The Id of the user that the tenant is connected to</param>
        /// <returns>Returns a tenant</returns>
        public Tenant FetchTenant(string userId)
        {
            //TODO: Check if you get apartmentID
            var tenant = FetchPerson(userId) as Tenant;
                
            if (tenant == null)
            {
                throw new InternalException();
            }

            return tenant;
        }

        /// <summary>
        /// </summary>Fetches all the Tenant from the database
        /// <returns>A list of Tenants</returns>
        public IEnumerable<Tenant> FetchTenants()
        {
            var tenants = _context.Tenants;
            return tenants;
        }

        /// <summary>
        /// Changes apartments for a Tenant
        /// </summary>
        /// <param name="userId">The Id of the User the tenant is connected to</param>
        /// <param name="apartmentId">The Id of the new apartment</param>
        /// <returns></returns>
        public Tenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = FetchTenant(userId);
            tenant.ChangeApartment(apartmentId);
            _context.SaveChanges();
            return tenant;
        }

        /// <summary>
        /// Fetches all the Tenant from a building
        /// </summary>
        /// <param name="buildingId">The Id of the Building</param>
        /// <returns>A list of Tenants</returns>
        public IEnumerable<Tenant> FetchTenants(int buildingId)
        {
            //TODO: Maybe put that in Building Register
            var tenants = _context.Tenants.Where(t => t.BuildingId.Equals(buildingId));
            return tenants; 
        }

        #endregion

        #region Booking

        /// <summary>
        /// Fetches all the bookings from a person
        /// </summary>
        /// <param name="userId">The Id of the user that the person is connected to</param>
        /// <returns>Returns a list of Bookings</returns>
        public IEnumerable<Booking> FetchBookings(string userId)
        {
            var person = _context.People
                                 .Include(p => p.Bookings)
                                 .FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            var bookings = person.Bookings;
            return bookings;
        }

        /// <summary>
        /// Fetches specific booking from a person
        /// </summary>
        /// <param name="userId">The Id of the user the person is connected to</param>
        /// <param name="bookingId">The Id of the booking</param>
        /// <returns>Returns a booking</returns>
        public Booking FetchBooking(string userId, int bookingId)
        {
            var person = _context.People.Include(p => p.Bookings)
                                 .FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person.FetchBooking(bookingId);
        }

        /// <summary>
        /// Cancels (Deletes) a booking
        /// </summary>
        /// <param name="userId">The Id of the user the person is connected to</param>
        /// <param name="bookingId">The Id of the booking</param>
        public void CancelBooking(string userId, int bookingId)
        {
            var person = _context.People.Include(p => p.Bookings)
                                 .FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();    
            }

            person.CancelBooking(bookingId);
            _context.SaveChanges();
        }

        #endregion

        #region Apartment

        /// <summary>
        /// Fetchs the apartment that a person lives in
        /// </summary>
        /// <param name="userId">The Id of the user the person is connected to</param>
        /// <returns>Returns the specified apartment</returns>
        public Apartment FetchApartment(string userId)
        {
            var tenant = _context.People.OfType<Tenant>()
                              .Include(a => a.Apartment)
                              .FirstOrDefault(a => a.UserId.Equals(userId));

            if (tenant == null)
            {
                throw new InternalException();
            }

            return tenant.Apartment;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
