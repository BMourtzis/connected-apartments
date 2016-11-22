using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    internal sealed class BuildingManager: Person, IBuildingManager
    {

        [ForeignKey("BuildingId")]
        internal Building Building { get; set; }

        #region Constructors

        private BuildingManager(): base() { }

        public BuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid,int buildingid) 
            :base(firstname, lastname, dateofbirth, newPhone, userid, buildingid) {}

        #endregion

        #region Properties

        #endregion

        #region Functions

        public void UpdateBuildingManager(string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            UpdatePerson(firstname, lastname, dateofbirth, newPhone);
        }

        #endregion
    }

    public interface IBuildingManager
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
        int BuildingId { get; }
    }
}
