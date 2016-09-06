using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingManager: Person, IBuildingManager
    {
        internal virtual Building building { get; set; }

        #region Constructors

        protected BuildingManager(): base() { }

        public BuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, Building newBuilding): base(firstname, lastname, dateofbirth, newPhone, userid)
        {
            building = newBuilding;
        }

        #endregion

        #region Properties

        IBuilding IBuildingManager.Building
        {
            get
            {
                return building;
            }
        }

        #endregion

        #region Functions



        #endregion
    }
}
