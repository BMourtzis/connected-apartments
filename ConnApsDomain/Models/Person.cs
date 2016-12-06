using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ConnApsDomain.Models
{
    internal abstract class Person: IPerson
    {
        internal virtual ICollection<Booking> Bookings { get; set; }

        #region Constructors

        protected Person() {}

        protected Person(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid, int buildingId)
        {
            FirstName = firstname;
            LastName = lastname;
            DoB = dateofbirth;
            Phone = newPhone;
            UserId = userid;
            BuildingId = buildingId;
        }

        #endregion

        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BuildingId { get;  private set; }

        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        public DateTime DoB { get; private set; }

        public string Phone { get; private set; }

        [Required]
        public string UserId { get; private set; }

        #endregion

        #region Functions

        public void UpdatePerson(string firstname, string lastname, DateTime dateofbirth, string phone)
        {
            if (firstname != null)
            {
                FirstName = firstname;
            }

            if (lastname != null)
            {
                LastName = lastname;
            }

            DoB = dateofbirth;

            if (phone != null)
            {
                Phone = phone;
            }
        }

        public Booking FetchBooking(int bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            var booking = FetchBooking(bookingId);
            Bookings.Remove(booking);
        }

        #endregion
    }

    public interface IPerson
    {
        string FirstName { get; }
        int BuildingId { get; }
        string LastName { get; }
        DateTime DoB { get; }
        string Phone { get; }
        string UserId { get; }
    }
}
