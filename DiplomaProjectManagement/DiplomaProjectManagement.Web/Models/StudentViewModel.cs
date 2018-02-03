using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Web.Models
{
    public class StudentViewModel
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

        public DateTime? CreatedDate { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]

        public string UpdatedBy { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }
    }
}