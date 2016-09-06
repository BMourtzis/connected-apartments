using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Tenant: Person, ITenant
    {
        internal virtual Apartment Aparment { get; set; }

        #region Constructors

        protected Tenant(): base() { }

        public Tenant(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, Apartment apt): base(firstname, lastname, dateofbirth, newPhone, userid)
        {
            Aparment = apt;
        }

        #endregion

        #region Properties

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
