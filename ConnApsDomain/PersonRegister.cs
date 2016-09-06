using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal class PersonRegister
    {


        #region Constructors

        public PersonRegister() { }

        #endregion

        #region Properties



        #endregion

        #region Functions

        public IBuildingManager addBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid)
        {
            BuildingManager bm;
            using (var context = new ConnApsContext())
            {
                bm = new BuildingManager(firstname, lastname, dateofbirth, newPhone, userid);
                context.BuildingManagers.Add(bm);
                context.SaveChanges();
            }
            return bm;
        }

        #endregion
    }
}
