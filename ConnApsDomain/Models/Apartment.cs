using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an instance of an apartment
    /// Inherets from Location
    /// </summary>
    internal sealed class Apartment: Location, IApartment
    {
        internal ICollection<Tenant> Tenants { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of the Apartment structure
        /// Used by Entity Framework
        /// </summary>
        private Apartment(): base() { }

        /// <summary>
        /// Initialises a new instance an Apartment Strcture
        /// </summary>
        /// <param name="level">The Level that the apartment is on</param>
        /// <param name="number">The Number of the Apartment</param>
        /// <param name="tenantsAllowed">The Number of tenants allowed to live in the apartment</param>
        /// <param name="facingdirection">The primary direction that the apartment is facing</param>
        /// <param name="buildingId">The Building Id the apartment is in</param>
        public Apartment(string level, string number, int tenantsAllowed, string facingdirection, int buildingId): base(level, number, buildingId)
        {
            TenantsAllowed = tenantsAllowed;
            FacingDirection = facingdirection;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Number of tenants allowed to live in the apartment
        /// </summary>
        [Required]
        public int TenantsAllowed { get; private set; }

        /// <summary>
        /// The primary direction that the apartment is facing
        /// </summary>
        [Required]
        public string FacingDirection { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the properties of the Apartment Structure
        /// </summary>
        /// <param name="level">The Level that the apartment is on</param>
        /// <param name="number">The Number of the Apartment</param>
        /// <param name="tenantsallowed">The Number of tenants allowed to live in the apartment</param>
        /// <param name="facingdirection">The primary direction that the apartment is facing</param>
        public void UpdateApartment(string level, string number, int tenantsallowed, string facingdirection)
        {
            UpdateLocation(level, number);
            TenantsAllowed = tenantsallowed;
            FacingDirection = facingdirection;
        }

        #endregion
    }

    /// <summary>
    /// Interface of the Apartment Structure
    /// </summary>
    public interface IApartment
    {
        /// <summary>
        /// The Id of the Apartment
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The Level that the apartment is on
        /// </summary>
        string Level { get; }

        /// <summary>
        /// The Number of the Apartment
        /// </summary>
        string Number { get; }

        /// <summary>
        /// The Building Id the apartment is in
        /// </summary>
        int BuildingId { get; }

        /// <summary>
        /// The Number of tenants allowed to live in the apartment
        /// </summary>
        int TenantsAllowed { get; }

        /// <summary>
        /// The primary direction that the apartment is facing
        /// </summary>
        string FacingDirection { get; }
    }
}
