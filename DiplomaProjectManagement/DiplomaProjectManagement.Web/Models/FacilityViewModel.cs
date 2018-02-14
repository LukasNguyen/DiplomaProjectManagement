using System;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(50)]
        public string Email { get; set; }

        [Url]
        public string UrlWebsite { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }
    }
}