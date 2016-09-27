using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    /// <summary>
    /// Represents an instance of an apartment
    /// Inherets from Location
    /// </summary>
    internal class Apartment: Location, IApartment
    {
        private int tenantsAllowed;
        private string facingDirection;
        
        internal virtual ICollection<Tenant> Tenants { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of the Apartment structure
        /// Used by Entity Framework
        /// </summary>
        protected Apartment(): base() { }

        /// <summary>
        /// Initialises a new instance an Apartment Strcture
        /// </summary>
        /// <param name="Level">The Level that the apartment is on</param>
        /// <param name="Number">The Number of the Apartment</param>
        /// <param name="tenantsallowed">The Number of tenants allowed to live in the apartment</param>
        /// <param name="facingdirection">The primary direction that the apartment is facing</param>
        /// <param name="buildingid">The Building Id the apartment is in</param>
        public Apartment(string Level, string Number, int tenantsallowed, string facingdirection, int buildingid): base(Level, Number, buildingid)
        {
            tenantsAllowed = tenantsallowed;
            facingDirection = facingdirection;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Number of tenants allowed to live in the apartment
        /// </summary>
        [Required]
        public int TenantsAllowed
        {
            get
            {
                return tenantsAllowed;
            }
            set
            {
                tenantsAllowed = value;
            }
        }

        /// <summary>
        /// The primary direction that the apartment is facing
        /// </summary>
        [Required]
        public string FacingDirection
        {
            get
            {
                return facingDirection;
            }
            set
            {
                facingDirection = value;
            }
        }

        /// <summary>
        /// A List of the Tenants living in the Apartment
        /// </summary>
        IEnumerable<ITenant> IApartment.Tenants
        {
            get
            {
                return Tenants;
            }
        }

        /// <summary>
        /// The Building that the apartment is in
        /// </summary>
        IBuilding IApartment.Building
        {
            get
            {
                return Building;
            }
        }

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
}
