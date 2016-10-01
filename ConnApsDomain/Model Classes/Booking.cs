using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnApsDomain
{
    internal class Booking : IBooking
    {
        private int id;
        private int facilityId;
        private int personId;
        private DateTime startTime;
        private DateTime endTime;

        [ForeignKey("FacilityId")]
        internal virtual Facility Facility { get; set; }

        [ForeignKey("PersonId")]
        internal virtual Person Person { get; set; }

        #region Constructors

        protected Booking() { }

        public Booking(int facilityid, int personid, DateTime starttime, DateTime endtime)
        {
            facilityId = facilityid;
            personId = personid;
            startTime = starttime;
            endTime = endtime;
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
        public int FacilityId
        {
            get
            {
                return facilityId;
            }
            set
            {
                facilityId = value;
            }
        }

        [Required]
        public int PersonId
        {
            get
            {
                return personId;
            }
            set
            {
                personId = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        IFacility IBooking.Facility
        {
            get
            {
                return Facility;
            }
        }

        IPerson IBooking.Person
        {
            get
            {
                return Person;
            }
        }

        #endregion

        #region Functions



        #endregion

    }
}