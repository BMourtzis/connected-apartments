using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{

    public class BuildingManagerRegisterModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6),
         DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm password"),
         Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(50, ErrorMessage = "The limit is {0} characters")]
        public string FirstName { get; set; }

        [Required, StringLength(50, ErrorMessage = "The limit is {0} characters")]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(10, ErrorMessage = "The phones length needs to be {0} digits long")]
        public string Phone { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }

    public class BuildingManagerUpdateModel: IValidatableObject
    {
        [Required]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == null && LastName == null && Phone == null && DateOfBirth == DateTime.Today)
            {
                yield return new ValidationResult(errorMessage: "All of the fields are empty. Please fill them in.");
            }
        }
    }
}