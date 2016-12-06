using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{
    public class RegisterBuildingModel
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

        [Required, StringLength(100, ErrorMessage = "The limit is {0} characters")]
        public string BuildingName { get; set; }

        [Required, StringLength(100, ErrorMessage = "The limit is {0} characters")]
        public string Address { get; set; }
    }

    public class BuildingBindingModel 
    {
        public string BuildingName { get; set; }
        public string Address { get; set; }
    }
}