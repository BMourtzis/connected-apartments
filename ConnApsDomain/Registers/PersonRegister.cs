using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        #region BuildingManager

        public IBuildingManager CreateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingId)
        {
            var bm = new BuildingManager(firstname, lastname, dateofbirth, newPhone, userid, buildingId);
            _context.BuildingManagers.Add(bm);
            _context.SaveChanges();
            return bm;
        }

        public IBuildingManager UpdateBuildingManager(string userId, string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            var bm = GetBuildingManager(userId);
            bm.UpdateBuildingManager(firstname, lastname, dateofbirth, newPhone);
            _context.SaveChanges();
            return bm;
        }

        public IBuildingManager FetchBuildingManager(string userId)
        {
            var bm = _context.BuildingManagers.FirstOrDefault(m => m.UserId.Equals(userId));
            return bm;

        }

        private BuildingManager GetBuildingManager(string userId)
        {
            var bm = _context.BuildingManagers.FirstOrDefault(m => m.UserId.Equals(userId));
            return bm;
        }

        public int FetchBuildingManagerBuildingId(string userId)
        {
            var bm = GetBuildingManager(userId);
            return bm.BuildingId;
        }

        public IBuilding FetchBuildingManagerBuilding(string userId)
        {
            throw new NotImplementedException();
            //var bm = FetchBuildingManager(userId);
            //return bm.Building;
        }

        public IEnumerable<IPerson> FetchBuildingBuildingManager(int buildingId)
        {
            var bms = _context.BuildingManagers.Where(bm => bm.BuildingId == buildingId);
            return bms;
        }

        #endregion

        #region Tenant

        public ITenant CreateTenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, int buildingId)
        {
            var tenant = new Tenant(firstName, lastName, dob, phone, userId, apartmentId, buildingId);
            _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return tenant;
        }

        public ITenant UpdateTenant(string userId, string firstName, string lastName, DateTime dob, string phone)
        {
            var tenant = GetTenant(userId);
            tenant.UpdateTenant(firstName, lastName, dob, phone);
            _context.SaveChanges();
            return tenant;
        }

        public int FetchTenantBuildingId(string userId)
        {
            var tenant = FetchTenant(userId);
            return tenant.BuildingId;
        }

        public ITenant FetchTenant(string userId)
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.UserId.Equals(userId));
            return tenant;
        }

        public IEnumerable<ITenant> FetchTenants()
        {
            var tenants = _context.Tenants;
            return tenants;
        }

        private Tenant GetTenant(string userId)
        {
            Tenant tenant = _context.Tenants.FirstOrDefault(t => t.UserId.Equals(userId));
            return tenant;
        }

        public ITenant ChangeApartment(string userId, int apartmentId)
        {
            var tenant = GetTenant(userId);
            tenant.ChangeApartment(apartmentId);
            _context.SaveChanges();
            return tenant;
        }

        public IEnumerable<ITenant> FetchBuildingTenants(int buildingId)
        {
            var tenants = _context.Tenants.Where(t => t.BuildingId.Equals(buildingId));
            return tenants;
        }

        #endregion

        #region Booking

        public IEnumerable<IBooking> FetchPersonBookings(string userId)
        {
            var person = _context.People
                                .Include(p => p.Bookings)
                                .FirstOrDefault(p => p.UserId.Equals(userId));
            var bookings = person.Bookings;
            return bookings;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
