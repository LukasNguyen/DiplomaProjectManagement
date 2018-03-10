using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class DiplomaProjectViewModel
    {
        public int ID { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập tên đề tài")]
        [DisplayName("Tên đồ án")]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập mô tả đề tài")]
        [DisplayName("Mô tả đồ án")]
        public string Description { get; set; }

        [DisplayName("Hiển thị trong đợt đăng ký")]
        public bool IsDisplayed { get; set; }

        public int LecturerId { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        public string LecturerName { get; set; }

        public ICollection<DiplomaProjectRegistrationViewModel> DiplomaProjectRegistration { get; set; }
    }
}