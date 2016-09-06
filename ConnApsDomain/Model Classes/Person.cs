using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    internal abstract class Person: IPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int personId;
        [Required]
        private string firstName;
        [Required]
        private string lastName;
        [Required]
        private DateTime dateOfBirth;
        private string phone;
        [Required]
        private string userId;

        #region Constructors

        protected Person() { }

        public Person(string firstname, string lastname, DateTime dateofbirth, string newPhone, string userid)
        {
            firstName = firstname;
            lastName = lastname;
            dateOfBirth = dateofbirth;
            phone = newPhone;
            userId = userid;
        }

        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        public DateTime DoB
        {
            get
            {
                return dateOfBirth;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
        }

        #endregion

        #region Functions

        public Person UpdateDetails(string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            firstName = firstname;
            lastName = lastname;
            dateOfBirth = dateofbirth;
            phone = newPhone;

            return this;
        }

        #endregion
    }
}
