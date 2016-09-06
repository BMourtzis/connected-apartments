using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingRegister
    {
        #region Constructors

        public BuildingRegister() { }

        #endregion

        #region Properties



        #endregion

        #region Functions

        public IBuilding addBuilding(string buildingname, string newAddress, int buildingmanagerid)
        {
            Building building;
            using (var context = new ConnApsContext())
            {
                building = new Building(buildingname, newAddress, buildingmanagerid);
                context.Buildings.Add(building);
                context.SaveChanges();
            }
            return building;
        }

        #endregion
    }
}
