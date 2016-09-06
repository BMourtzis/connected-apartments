using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal abstract class Location: ILocation
    {
        private string level;
        private string number;

        internal virtual Building Building { get; set; }

        #region Constructors

        protected Location () { }

        public Location(string newLevel, string newNumber, Building building)
        {
            level = newLevel;
            number = newNumber;
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
