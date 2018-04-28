using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class LecturerViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên giảng viên.")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [MaxLength(250)]
        public string Address { get; set; }

        public ICollection<DiplomaProjectViewModel> DiplomaProjects { get; set; }

        public int FacilityId { get; set; }

        public FacilityViewModel Facility { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }

    public class LecturerLoginViewModel : LecturerViewModel
    {
        public string ApplicationUserId { get; set; }

        public string Password { get; set; }
    }
}