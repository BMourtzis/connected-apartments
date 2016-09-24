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
        internal virtual Apartment Apartment { get; set; }

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

        public int BuildingId
        {
            get
            {
                if(Apartment != null)
                {
                    return Apartment.BuildingId;
                }
                else
                {
                    return 0;
                }
            }
        }


        IApartment ITenant.Apartment
        {
            get
            {
                return Apartment;
            }
        }

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
}
