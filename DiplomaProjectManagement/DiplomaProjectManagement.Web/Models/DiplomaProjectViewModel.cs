using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Web.Models
{
    public class DiplomaProjectViewModel
    {
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        [DisplayName("Tên đồ án")]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        [DisplayName("Mô tả đồ án")]
        public string Description { get; set; }

        [DisplayName("Số sinh viên đăng ký")]
        public int numberOfStudentsRegistered { get; set; }

        public int LecturerId { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        public ICollection<DiplomaProjectRegistrationViewModel> DiplomaProjectRegistration { get; set; }
    }
}