using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [Required]
        public int BuildingId { get; set; }
    }

    public class ApartmentUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int TenantsAllowed { get; set; }

        [Required]
        public string FacingDirection { get; set; }
    }

    public class TenantUpdateModel
    {
        [Required]
        public string UserId { get; set; }

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

    public class BuildingManagerBindingModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DoB { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class BuildingBindingModel
    {
        public string BuildingName { get; set; }
        public string Address { get; set; }
    }
}