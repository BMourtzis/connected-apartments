using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain.Models
{
    internal sealed class Booking : IBooking
    {
        [ForeignKey("FacilityId")]
        internal Facility Facility { get; set; }

        [ForeignKey("PersonId")]
        internal Person Person { get; set; }

        #region Constructors

        private Booking() { }

        public Booking(int facilityid, int personid, DateTime starttime, DateTime endtime)
        {
            FacilityId = facilityid;
            PersonId = personid;
            StartTime = starttime;
            EndTime = endtime;
        }

        #endregion

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FacilityId { get; private set; }

        [Required]
        public int PersonId { get; private set; }

        [Required]
        public DateTime StartTime { get; private set; }

        [Required]
        public DateTime EndTime { get; private set; }

        #endregion

        #region Functions



        #endregion

    }

    public interface IBooking
    {
        int Id { get; }
        int FacilityId { get; }
        int PersonId { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
    }
}