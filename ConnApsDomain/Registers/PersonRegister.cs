using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConnApsDomain.Exceptions;
using ConnApsDomain.Models;

namespace ConnApsDomain.Registers
{
    internal class PersonRegister: IDisposable
    {

        #region Constructors

        public PersonRegister()
        {
            _context = new ConnApsContext();
        }

        #endregion

        #region Properties

        private readonly ConnApsContext _context;

        #endregion

        #region Person

        public int FetchBuildingId(string userId)
        {
            var person = _context.People.FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person.BuildingId;
        }

        public Person FetchPerson(string userId)
        {
            var person = _context.People.FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person;
        }

        public void UpdatePerson(string firstname, string lastname, DateTime dateofbirth, string phone, string userId)
        {
            var person = FetchPerson(userId);
            person.UpdatePerson(firstname, lastname, dateofbirth, phone);
        }

        #endregion

        #region BuildingManager

        public BuildingManager CreateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingId)
        {
            var bm = new BuildingManager(firstname, lastname, dateofbirth, newPhone, userid, buildingId);
            _context.BuildingManagers.Add(bm);
            _context.SaveChanges();
            return bm;
        }

        public BuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var bm = FetchBuildingManager(userId);
            bm.UpdatePerson(firstname, lastname, dateofbirth, newPhone);
            _context.SaveChanges();
            return bm;
        }

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

        public IEnumerable<BuildingManager> FetchBuildingManagers()
        {
            var bms = _context.BuildingManagers;
            return bms;
        }

        #endregion

        #region Tenant

        public Tenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, int buildingId)
        {
            var tenant = new Tenant(firstName, lastName, dob, phone, userId, apartmentId, buildingId);
            _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return tenant;
        }

        public Tenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = FetchTenant(userId);
            tenant.UpdatePerson(firstName, lastName, dob, phone);
            _context.SaveChanges();
            return tenant;
        }

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

        public IEnumerable<Tenant> FetchTenants()
        {
            var tenants = _context.Tenants;
            return tenants;
        }

        public Tenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = FetchTenant(userId);
            tenant.ChangeApartment(apartmentId);
            _context.SaveChanges();
            return tenant;
        }

        public IEnumerable<Tenant> FetchTenants(int buildingId)
        {
            //TODO: Maybe put that in Building Register
            var tenants = _context.Tenants.Where(t => t.BuildingId.Equals(buildingId));
            return tenants; 
        }

        #endregion

        #region Booking

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

        public IBooking FetchBooking(string userId, int bookingId)
        {
            var person = _context.People.Include(p => p.Bookings)
                                 .FirstOrDefault(p => p.UserId.Equals(userId));

            if (person == null)
            {
                throw new InternalException();
            }

            return person.FetchBooking(bookingId);
        }

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

        public IApartment FetchApartment(string userId)
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
