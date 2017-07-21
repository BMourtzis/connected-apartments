using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ConnApsDomain.Exceptions;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents an instance of a Person
    /// </summary>
    internal abstract class Person: IPerson
    {
        /// <summary>
        /// A list of bookings that belong to the Person
        /// </summary>
        internal virtual ICollection<Booking> Bookings { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Person
        /// Used by Entity Framework
        /// </summary>
        protected Person() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName">The First Name of the Person</param>
        /// <param name="lastName">The Last Name of the Person</param>
        /// <param name="dob">The Date of Birth of the Person</param>
        /// <param name="phone">The Phone of the Person</param>
        /// <param name="userId">The Id of the User that the Person is connected to</param>
        /// <param name="buildingId">The Id of the Building the Person is connected to</param>
        protected Person(string firstName, string lastName, DateTime dob, string phone, string userId, int buildingId)
        {
            FirstName = firstName;
            LastName = lastName;
            DoB = dob;
            Phone = phone;
            UserId = userId;
            BuildingId = buildingId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the person
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //TODO: Should be made not Required later
        /// <summary>
        /// The ID of the Buidling the person belongs to
        /// </summary>
        [Required]
        public int BuildingId { get;  private set; }

        /// <summary>
        /// The First Name of the Person
        /// </summary>
        [Required]
        public string FirstName { get; private set; }

        /// <summary>
        /// The last name of the Person
        /// </summary>
        [Required]
        public string LastName { get; private set; }

        /// <summary>
        /// The Date of Birth of the Person
        /// </summary>
        [Required]
        public DateTime DoB { get; private set; }

        /// <summary>
        /// The Phone of the Person
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// The UserId of the user the Person is connected to.
        /// Part of the ASP.Net Identity
        /// </summary>
        [Required]
        public string UserId { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Updates information of the Person object
        /// </summary>
        /// <param name="firstName">The First Name of the Person</param>
        /// <param name="lastName">The Last Name of the Person</param>
        /// <param name="dob">The Date of Birth of the Person</param>
        /// <param name="phone">The Phone of the Person</param>
        public void UpdatePerson(string firstName, string lastName, DateTime dob, string phone)
        {
            if (firstName != null)
            {
                FirstName = firstName;
            }

            if (lastName != null)
            {
                LastName = lastName;
            }

            DoB = dob;

            if (phone != null)
            {
                Phone = phone;
            }
        }

        /// <summary>
        /// Fetches bookings of the Person
        /// </summary>
        /// <param name="bookingId">The ID of the booking</param>
        /// <returns>Returns a booking object</returns>
        public Booking FetchBooking(int bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking");
            }

            return booking;
        }

        /// <summary>
        /// Cancels a booking made by the user
        /// </summary>
        /// <param name="bookingId">The ID of the Booking</param>
        public void CancelBooking(int bookingId)
        {
            var booking = FetchBooking(bookingId);
            Bookings.Remove(booking);
        }

        #endregion
    }

    public interface IPerson
    {
        /// <summary>
        /// The ID of the Person
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The First Name of the Person
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The ID of the Building the Person is part of
        /// </summary>
        int BuildingId { get; }

        /// <summary>
        /// The Last Name of the Person
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// The Date of Birth of the Person
        /// </summary>
        DateTime DoB { get; }

        /// <summary>
        /// The Phone of th Person
        /// </summary>
        string Phone { get; }

        /// <summary>
        /// The User ID of the User that the person is connected to
        /// </summary>
        string UserId { get; }
    }
}
