using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an Instance of the Building Manager.
    /// Inherets from Person.
    /// </summary>
    internal sealed class BuildingManager: Person, IBuildingManager
    {
        /// <summary>
        /// A Representation of the Building the Building Manager manages.
        /// The Building needs to be loaded explicitly
        /// </summary>
        [ForeignKey("BuildingId")]
        internal Building Building { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Building Manager
        /// Used by Entity Framework
        /// </summary>
        private BuildingManager(): base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName">The First Name of the Building Manager</param>
        /// <param name="lastName">The Last Name of the Building Manager</param>
        /// <param name="dob">The Date of Birth of the Building Manager</param>
        /// <param name="phone">The Phone of the Building Manager</param>
        /// <param name="userId">The User Id of the Building Manager</param>
        /// <param name="buildingId">The Id of the Building the Building Manager is part of</param>
        public BuildingManager(string firstName, string lastName, DateTime dob, string phone, string userId,int buildingId) 
            :base(firstName, lastName, dob, phone, userId, buildingId) {}

        #endregion

        #region Properties

        #endregion

        #region Functions

        #endregion
    }

    public interface IBuildingManager
    {
        /// <summary>
        /// The ID of the Building Manager (Person)
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The First Name of the Building Manager (Person)
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The Last Name of the Building Manager (Person)
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// The Date of Birth of the Building Manager (Person)
        /// </summary>
        DateTime DoB { get; }

        /// <summary>
        /// The Phone of the Building Manager (Person)
        /// </summary>
        string Phone { get; }

        /// <summary>
        /// The User ID of the User the Building Manager (Person) is connected to 
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// The ID of the building the Building Manager (Person) belongs to.
        /// </summary>
        int BuildingId { get; }
    }
}
