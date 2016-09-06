using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Apartment: Location, IApartment
    {
        private int tenantsAllowed;
        private string facingDirection;

        internal virtual Tenant Tenant { get; set; }

        #region Constructors

        protected Apartment(): base() { }

        public Apartment(string newLevel, string newNumber, int tenantsallowed, string facingdirection, Building building): base(newLevel, newNumber, building)
        {
            tenantsAllowed = tenantsallowed;
            facingDirection = facingdirection;
        }

        #endregion

        #region Properties

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
