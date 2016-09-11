using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Tenant: Person, ITenant
    {
        private int apartmentId;

        [ForeignKey("ApartmentId")]
        internal virtual Apartment Aparment { get; set; }

        #region Constructors

        protected Tenant(): base() { }

        public Tenant(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int apartmentid): base(firstname, lastname, dateofbirth, newPhone, userid)
        {
            apartmentId = apartmentid;
        }

        #endregion

        #region Properties

        public int ApartmentId
        {
            get
            {
                return apartmentId;
            }
            set
            {
                apartmentId = value;
            }
        }

        IApartment ITenant.Apartment
        {
            get
            {
                return Aparment;
            }
        }

        #endregion

        #region Functions



        #endregion
    }
}
