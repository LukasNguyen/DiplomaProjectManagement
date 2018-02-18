using System;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class LecturerViewModel
    {
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public ICollection<DiplomaProject> DiplomaProjects { get; set; }

        public int FacilityId { get; set; }

        public Facility Facility { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]

        public string UpdatedBy { get; set; }

        [MaxLength(256)]

        public string CreatedBy { get; set; }

        public bool Status { get; set; }
    }

    public class LecturerLoginViewModel : LecturerViewModel
    {
        public string ApplicationUserId { get; set; }

        public string Password { get; set; }
    }
}