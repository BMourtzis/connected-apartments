using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Building: IBuilding
    {
        private int id;
        private string buildingName;
        private string address;

        internal List<Location> Locations { get; set; }
        internal List<BuildingManager> Managers { get; set; }

        #region Constructors

        protected Building() { }

        public Building(string buildingname, string newAddress)
        {
            buildingName = buildingname;
            address = newAddress;
        }

        #endregion

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [Required]
        public string BuildingName
        {
            get
            {
                return buildingName;
            }
            set
            {
                buildingName = value;
            }
        }

        [Required]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public IEnumerable<Apartment> Apartments
        {
            get
            {
                if(Locations != null )
                {
                    return Locations.OfType<Apartment>();
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Facility> Facilities
        {
            get
            {
                if (Locations != null)
                {
                    return Locations.OfType<Facility>();
                }
                else
                {
                    return null;
                }
            }
        }

        IEnumerable<IApartment> IBuilding.Apartments
        {
            get
            {
                return Apartments;
            }
        }

        IEnumerable<IFacility> IBuilding.Facilities
        {
            get
            {
                return Facilities;
            }
        }

        IEnumerable<IBuildingManager> IBuilding.BuildingManagers
        {
            get
            {
                return Managers;
            }
        }


        #endregion

        #region Functions

        public void UpdateBuilding(string buildingName, string address)
        {
            BuildingName = buildingName;
            Address = address;
        }

        public Apartment CreateApartment(string Level, string Number, int TenantsAllowed, string FacingDirection)
        {
            Apartment ap = new Apartment(Level, Number, TenantsAllowed, FacingDirection, Id);
            return ap;
        }

        public Facility CreateFacility(string Level, string Number)
        {
            Facility f = new Facility(Level, Number, Id);
            return f;
        }

        public Booking CreateBooking(int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            var facility = Facilities.Where(f => f.Id == facilityId).FirstOrDefault();
            var booking = facility.CreateBooking(personId, startTime, endTime);
            return booking;
        }

        #endregion


    }
}
