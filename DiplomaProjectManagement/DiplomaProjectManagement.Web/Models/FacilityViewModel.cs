using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomaProjectManagement.Web.Models
{
    public class FacilityViewModel
    {
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public string LocationBuilding { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string UrlWebsite { get; set; }
    }
}