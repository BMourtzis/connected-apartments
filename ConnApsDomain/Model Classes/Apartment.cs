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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int apartmentId;
        [Required]
        private int tenantsAllowed;
        private string facingDirection;
        private int tenantId;
        
        [ForeignKey("tenantId")]
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

        public int ApartmentId
        {
            get
            {
                return apartmentId;
            }
        }

        public int TenantsAllowed
        {
            get
            {
                return tenantsAllowed;
            }
        }

        public string FacingDirection
        {
            get
            {
                return facingDirection;
            }
        }

        public int TenantId
        {
            get
            {
                return tenantId;
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
