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

        private int buildingId;

        [ForeignKey("BuildingId")]
        internal virtual Building Building { get; set; }

        #region Constructors

        protected BuildingManager(): base() { }

        public BuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingid): base(firstname, lastname, dateofbirth, newPhone, userid)
        {
            buildingId = buildingid;
        }

        #endregion

        #region Properties

        [Required]
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

        IBuilding IBuildingManager.Building
        {
            get
            {
                return Building;
            }
        }

        #endregion

        #region Functions

        public void UpdateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            UpdatePerson(firstname, lastname, dateofbirth, newPhone);
        }

        #endregion
    }
}
