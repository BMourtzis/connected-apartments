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
        private int id;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string phone;
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
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        [Required]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        [Required]
        public DateTime DoB
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        [Required]
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        #endregion

        #region Functions

        protected void UpdatePerson(string firstname, string lastname, DateTime dateofbirth, string newPhone)
        {
            firstName = firstname;
            lastName = lastname;
            dateOfBirth = dateofbirth;
            phone = newPhone;
        }

        #endregion
    }
}
