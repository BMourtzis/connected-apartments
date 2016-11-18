using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    internal sealed class Tenant: Person, ITenant
    {
        [ForeignKey("ApartmentId")]
        internal Apartment Apartment { get; set; }

        #region Constructors

        private Tenant(): base() { }

        public Tenant(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int apartmentid, int buildingId)
            :base(firstname, lastname, dateofbirth, newPhone, userid, buildingId)
        {
            ApartmentId = apartmentid;
        }

        #endregion

        #region Properties

        public int ApartmentId { get; private set; }

        #endregion

        #region Functions

        public void UpdateTenant(string firstName, string lastName, DateTime dob, string phone)
        {
            UpdatePerson(firstName, lastName, dob, phone);
        }

        public void ChangeApartment(int aptId)
        {
            ApartmentId = aptId;
        }

        #endregion
    }

    public interface ITenant
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        int ApartmentId { get; }
        string UserId { get; }
        int BuildingId { get; }
    }
}
