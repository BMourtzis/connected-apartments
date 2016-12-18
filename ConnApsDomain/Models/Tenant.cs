using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an instance of a Tenant
    /// Inherents from Person
    /// </summary>
    internal sealed class Tenant: Person, ITenant
    {
        /// <summary>
        /// The apartment that the tenant lives in
        /// </summary>
        [ForeignKey("ApartmentId")]
        internal Apartment Apartment { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Tenant
        /// Used by Entity Framework
        /// </summary>
        private Tenant(): base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName">The First Name of the Tenant</param>
        /// <param name="lastName">The Last Name of the Tenant</param>
        /// <param name="dob">The Date of Birth of the Tenant</param>
        /// <param name="phone">The phone of the Tenant</param>
        /// <param name="userId">The Id of the User that connects to</param>
        /// <param name="apartmentId">The Id of the Apartment the tenant lives in</param>
        /// <param name="buildingId">The Id of the Building that the tenant is part of</param>
        public Tenant(string firstName, string lastName, DateTime dob, string phone, string userId, int apartmentId, int buildingId)
            :base(firstName, lastName, dob, phone, userId, buildingId)
        {
            ApartmentId = apartmentId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Id of the apartment the tenant lives in
        /// </summary>
        public int ApartmentId { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Changes the apartment the tenant lives in
        /// </summary>
        /// <param name="aptId">The Id of the new apartment</param>
        public void ChangeApartment(int aptId)
        {
            ApartmentId = aptId;
        }

        #endregion
    }

    public interface ITenant
    {
        /// <summary>
        /// The ID of the tenant (Person)
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The First Name of the tenant (Person)
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The Last Name of the tenant (Person)
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// The Date of Birth of the tenant (Person)
        /// </summary>
        DateTime DoB { get; }

        /// <summary>
        /// The Phone of the tenant (Person)
        /// </summary>
        string Phone { get; }

        /// <summary>
        /// The ID of the apartment the tenant lives in
        /// </summary>
        int ApartmentId { get; }

        /// <summary>
        /// The Id of the user the tenant is connected to
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// The Id of the Building the tenant is part of
        /// </summary>
        int BuildingId { get; }
    }
}
