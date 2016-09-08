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
        private int tenantId;
        
        [ForeignKey("TenantId")]
        internal virtual Tenant Tenant { get; set; }

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

        public int TenantId
        {
            get
            {
                return tenantId;
            }
            set
            {
                tenantId = value;
            }
        }

        ITenant IApartment.Tenant
        {
            get
            {
                return Tenant;
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



        #endregion
    }
}
