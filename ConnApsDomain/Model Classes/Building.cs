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

        internal virtual ICollection<Location> Locations { get; set; }
        internal virtual ICollection<BuildingManager> Managers { get; set; }

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

        IEnumerable<ILocation> IBuilding.Locations
        {
            get
            {
                return Locations;
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

        public Apartment CreateApartment(string Level, string Number, int TenantsAllowed, string FacingDirection)
        {
            Apartment ap = new Apartment(Level, Number, TenantsAllowed, FacingDirection, Id);
            return ap;
        }

        #endregion


    }
}
