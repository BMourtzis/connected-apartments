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
        private int buildingId;
        private string buildingName;
        private string address;
        private int buildingManagerId;

        internal virtual ICollection<Location> Locations { get; set; }

        [ForeignKey("BuildingManagerId")]
        internal virtual BuildingManager Manager { get; set; }

        #region Constructors
        
        protected Building() { }

        public Building(string buildingname, string newAddress, int buildingmanagerid)
        {
            buildingName = buildingname;
            address = newAddress;
            buildingManagerId = buildingmanagerid;
        }

        #endregion

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildingId
        {
            get
            {
                return buildingId;
            }
            set
            {
                buildingId = value;
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

        public int BuildingManagerId
        {
            get
            {
                return buildingManagerId;
            }
            set
            {
                buildingManagerId = value;
            }
        }

        IEnumerable<ILocation> IBuilding.Locations
        {
            get
            {
                return Locations;
            }
        }

        IBuildingManager IBuilding.BuildingManager
        {
            get
            {
                return Manager;
            }
        }


        #endregion

        #region Functions

        public Apartment createApartment()
        {
            return null;
        }

        #endregion


    }
}
