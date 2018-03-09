using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Tên sinh viên")]
        [Required(ErrorMessage = "Vui lòng nhập tên sinh viên")]
        public string Name { get; set; }

        [MaxLength(50)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(250)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }

    }

    public class StudentLoginViewModel : StudentViewModel
    {
        public string ApplicationUserId { get; set; }

        public string Password { get; set; }
    }
}