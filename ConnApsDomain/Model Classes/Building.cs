using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class Building: IBuilding
    {
        private string buildingName;
        private string address;

        internal virtual ICollection<Location> Locations { get; set; }
        internal virtual BuildingManager Manager { get; set; }

        #region Constructors

        protected Building() { }

        public Building(string buildingname, string newAddress)
        {
            buildingName = buildingname;
            address = newAddress;
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
