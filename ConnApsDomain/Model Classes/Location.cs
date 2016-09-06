using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal abstract class Location: ILocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int locationId;
        private string level;
        private string number;
        private int buildingId;

        [ForeignKey("buildingId")]
        internal virtual Building Building { get; set; }

        #region Constructors

        protected Location () { }

        public Location(string newLevel, string newNumber, int buildingid)
        {
            level = newLevel;
            number = newNumber;
            buildingId = buildingid;
        }

        #endregion

        #region Properties

        public string Level
        {
            get
            {
                return level;
            }
        }

        public string Number
        {
            get
            {
                return number;
            }
        }

        public int BuildingId
        {
            get
            {
                return buildingId;
            }
        }

        IBuilding ILocation.Building
        {
            get
            {
                return Building;
            }
        }

        #endregion

        #region Functions



        #endregion
    }
}
