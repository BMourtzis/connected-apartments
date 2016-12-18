using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    /// <summary>
    /// Represents a booking Instance
    /// </summary>
    internal sealed class Booking : IBooking
    {
        /// <summary>
        /// The Facility that the booking is for
        /// </summary>
        [ForeignKey("FacilityId")]
        internal Facility Facility { get; set; }

        /// <summary>
        /// The Person that made the booking
        /// </summary>
        [ForeignKey("PersonId")]
        internal Person Person { get; set; }

        #region Constructors

        /// <summary>
        /// Initialises an empty instance of Booking
        /// Used by Entity Framework
        /// </summary>
        private Booking() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityId">The ID of the Facility that the booking is for</param>
        /// <param name="personId">The ID of the Person tha makes the booking</param>
        /// <param name="startTime">The Datetime that the booking starts</param>
        /// <param name="endTime">The Datetime that the booking ends</param>
        public Booking(int facilityId, int personId, DateTime startTime, DateTime endTime)
        {
            FacilityId = facilityId;
            PersonId = personId;
            StartTime = startTime;
            EndTime = endTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the booking
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the facility that the bookings is for
        /// </summary>
        [Required]
        public int FacilityId { get; private set; }

        /// <summary>
        /// The ID of the person that made the booking
        /// </summary>
        [Required]
        public int PersonId { get; private set; }

        /// <summary>
        /// The Datetime the booking starts
        /// </summary>
        [Required]
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// The Datetime the booking ends
        /// </summary>
        [Required]
        public DateTime EndTime { get; private set; }

        #endregion

        #region Functions



        #endregion

    }

    public interface IBooking
    {
        /// <summary>
        /// The ID of the booking
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The ID of the facility that the bookings is for
        /// </summary>
        int FacilityId { get; }

        /// <summary>
        /// The ID of the person that made the booking
        /// </summary>
        int PersonId { get; }

        /// <summary>
        /// The Datetime that the booking starts
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// The Datetime the booking ends
        /// </summary>
        DateTime EndTime { get; }
    }
}