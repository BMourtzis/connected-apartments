using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{
    public class RegisterTenantModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DoB { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int ApartmentId { get; set; }
    }

    public class ChangeApartmentModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ApartmentId { get; set; }
    }

    public class TenantUpdateModel: IValidatableObject
    {
        [Required]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Check what's the default value of a datetime object
            if(FirstName == null && LastName == null && DateofBirth == DateTime.MinValue && Phone == null)
            {
                yield return new ValidationResult(errorMessage: "All of the fields are empty. Please fill them in.");
            }
        }
    }
}