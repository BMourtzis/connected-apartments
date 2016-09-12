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
        private int id;
        private string level;
        private string number;

        [Required]
        [ForeignKey("Building")]
        private int buildingId;

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
        public string Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        [Required]
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

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

        IBuilding ILocation.Building
        {
            get
            {
                return Building;
            }
        }

        #endregion

        #region Functions

        protected void UpdateLocation(string level, string number)
        {
            Level = level;
            Number = number;
        }

        #endregion
    }
}