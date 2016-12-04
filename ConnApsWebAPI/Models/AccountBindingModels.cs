using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConnApsDomain.Models;
using Newtonsoft.Json;

namespace ConnApsWebAPI.Models
{
    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class PersonInformationModel: UserInfoViewModel
    {
        protected PersonInformationModel() {}

        public PersonInformationModel(UserInfoViewModel model, IBuildingManager user)
        {
            Email = model.Email;
            HasRegistered = model.HasRegistered;
            LoginProvider = model.LoginProvider;
            Roles = model.Roles;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BuildingId = user.BuildingId;
            DoB = user.DoB;
            Phone = user.Phone;
            UserId = user.UserId;
        }

        public string FirstName { get; set; }
        public int BuildingId { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Phone { get; set; }
        public string UserId { get; set; }
    }

    public class TenantInformationModel : PersonInformationModel
    {
        public TenantInformationModel(UserInfoViewModel model, ITenant user)
        {
            Email = model.Email;
            HasRegistered = model.HasRegistered;
            LoginProvider = model.LoginProvider;
            Roles = model.Roles;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BuildingId = user.BuildingId;
            DoB = user.DoB;
            Phone = user.Phone;
            UserId = user.UserId;
            ApartmentId = user.ApartmentId;
        }

        public int ApartmentId { get; set; }
    }

    public class EditAccountModel: IValidatableObject
    {
        [StringLength(50, ErrorMessage = "The limit is {0} characters")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The limit is {0} characters")]
        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        [StringLength(10, ErrorMessage = "The phones length needs to be {0} digits long")]
        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == null && LastName == null && Phone == null && DoB == DateTime.MinValue)
            {
                yield return new ValidationResult(errorMessage: "All of the fields are empty. Please fill them in.");
            }
        }
    }
}
