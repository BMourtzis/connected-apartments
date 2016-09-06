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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int buildingId;
        [Required]
        private string buildingName;
        [Required]
        private string address;
        [Required]
        private int buildingManagerId;

        internal virtual ICollection<Location> Locations { get; set; }

        [ForeignKey("buildingManagerId")]
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

        public string BuildingName
        {
            get
            {
                return buildingName;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
        }

        public int BuildingId
        {
            get
            {
                return buildingId;
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
