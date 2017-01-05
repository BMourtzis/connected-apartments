using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnApsWebAPI.Models
{
    public class ApartmentBindingModel
    {
        [Required]
        public string Level { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int TenantsAllowed { get; set; }

        [Required]
        public string FacingDirection { get; set; }
    }

    public class ApartmentUpdateModel: IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        public string Level { get; set; }

        public string Number { get; set; }

        public int TenantsAllowed { get; set; }

        public string FacingDirection { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Level == null && Number == null && FacingDirection == null && TenantsAllowed == 0)
            {
                yield return new ValidationResult( errorMessage: "All of the fields are empty. Please fill them in.");
            }
        }
    }

    public class FacilityRegisterModel
    {
        [Required]
        public string Level { get; set; }
        [Required]
        public string Number { get; set; }
    }

    public class FacilityUpdateModel: IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        public string Level { get; set; }

        public string Number { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Level == null && Number == null)
            {
                yield return new ValidationResult(errorMessage: "All of the fields are empty. Please fill them in.");
            }
        }
    }
}