using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    internal abstract class Location: ILocation
    {
        [ForeignKey("BuildingId")]
        internal virtual Building Building { get; set; }

        #region Constructors

        protected Location () { }

        protected Location(string newLevel, string newNumber, int buildingId)
        {
            Level = newLevel;
            Number = newNumber;
            BuildingId = buildingId;
        }

        #endregion

        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Level { get; private set; }

        [Required]
        public string Number { get; private set; }

        [Required]
        public int BuildingId { get; private set; }

        #endregion

        #region Functions

        protected void UpdateLocation(string level, string number)
        {
            Level = level;
            Number = number;
        }

        #endregion
    }
    public interface ILocation
    {
        int Id { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
    }
}