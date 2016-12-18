using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Abstract Class.
    /// It represents a location within a Building
    /// </summary>
    internal abstract class Location: ILocation
    {
        /// <summary>
        /// The Instance of a building the Location is part of
        /// It needs to be explicitly loaded
        /// </summary>
        [ForeignKey("BuildingId")]
        internal virtual Building Building { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Location
        /// Used by Entity Framework
        /// </summary>
        protected Location () { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">The Level that the Location is on</param>
        /// <param name="number">The Number of the Location</param>
        /// <param name="buildingId">The Id of the Building the Location is a part of</param>
        protected Location(string level, string number, int buildingId)
        {
            Level = level;
            Number = number;
            BuildingId = buildingId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the Location object
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The Level the Location in on
        /// </summary>
        [Required]
        public string Level { get; private set; }

        /// <summary>
        /// The Number of the location within the building and Level
        /// </summary>
        [Required]
        public string Number { get; private set; }

        /// <summary>
        /// The Id of the Building the Location is part of
        /// </summary>
        [Required]
        public int BuildingId { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Update information of the Location instance
        /// </summary>
        /// <param name="level">The Level Location is on</param>
        /// <param name="number">The Number of the Location</param>
        protected void UpdateLocation(string level, string number)
        {
            Level = level;
            Number = number;
        }

        #endregion
    }
    public interface ILocation
    {
        /// <summary>
        /// The ID of the Location
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The Level the Location in on
        /// </summary>
        string Level { get; }

        /// <summary>
        /// The Number of the Location
        /// </summary>
        string Number { get; }

        /// <summary>
        /// The ID of the Building the Location is part of
        /// </summary>
        int BuildingId { get; }
    }
}