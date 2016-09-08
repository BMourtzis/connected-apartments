using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class BuildingManager: Person, IBuildingManager
    {
        private int buildingManagerId;

        internal virtual Building building { get; set; }

        #region Constructors

        protected BuildingManager(): base() { }

        public BuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid): base(firstname, lastname, dateofbirth, newPhone, userid)
        {
            //buildingId = buildingid;
        }

        #endregion

        #region Properties

        //public int BuildingManagerId
        //{
        //    get
        //    {
        //        return buildingManagerId;
        //    }
        //}

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
