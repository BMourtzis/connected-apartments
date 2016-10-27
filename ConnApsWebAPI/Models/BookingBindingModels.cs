using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{
    public class BookingCreateModel : IValidatableObject
    {
        [Required]
        public int FacilityId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime < StartTime)
            {
                yield return new ValidationResult(errorMessage: "EndTime must be greater than StartDate", memberNames: new[] { "EndTime" });
            }
        }
    }

    public class BookingCancelModel
    {
        [Required]
        public int FacilityId { get; set; }
        [Required]
        public int BookingId { get; set; }
    }
}