using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class StudentViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sinh viên.")]
        [MaxLength(250)]
        [DisplayName("Tên sinh viên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [MaxLength(11)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [MaxLength(250)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public float? GPA { get; set; }

        public bool Status { get; set; }
    }

    public class StudentLoginViewModel : StudentViewModel
    {
        public string ApplicationUserId { get; set; }

        public string Password { get; set; }
    }
}