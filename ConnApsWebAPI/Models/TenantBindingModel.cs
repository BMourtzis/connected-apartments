﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnApsWebAPI.Models
{
    public class TenantUpdateModel
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
}