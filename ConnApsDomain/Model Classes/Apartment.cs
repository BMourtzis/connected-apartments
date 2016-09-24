using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Apartment: Location, IApartment
    {
        private int tenantsAllowed;
        private string facingDirection;
        
        internal virtual ICollection<Tenant> Tenants { get; set; }

        #region Constructors

        protected Apartment(): base() { }

        public Apartment(string newLevel, string newNumber, int tenantsallowed, string facingdirection, int buildingid): base(newLevel, newNumber, buildingid)
        {
            tenantsAllowed = tenantsallowed;
            facingDirection = facingdirection;
        }

        #endregion

        #region Properties

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

        IEnumerable<ITenant> IApartment.Tenants
        {
            get
            {
                return Tenants;
            }
        }

        IBuilding IApartment.Building
        {
            get
            {
                return Building;
            }
        }

        #endregion

        #region Functions

        public void UpdateApartment(string level, string number, int tenantsallowed, string facingdirection)
        {
            UpdateLocation(level, number);
            TenantsAllowed = tenantsallowed;
            FacingDirection = facingdirection;
        }

        #endregion
    }
}
