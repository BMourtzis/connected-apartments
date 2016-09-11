﻿using System;
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
        internal virtual Building building { get; set; }

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
                return building;
            }
        }

        #endregion

        #region Functions



        #endregion
    }
}
