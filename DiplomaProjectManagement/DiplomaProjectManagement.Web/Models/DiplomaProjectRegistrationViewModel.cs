using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class DiplomaProjectRegistrationViewModel
    {
        public int DiplomaProjectId { get; set; }

        public int StudentId { get; set; }

        public int RegistrationTimeId { get; set; }

        public float? IntroducedGrades { get; set; }

        public float? ReviewedGrades { get; set; }

        public string TeamName { get; set; }
    }

    public class DiplomaProjectTeamRegistrationViewModel
    {
        public int DiplomaProjectId { get; set; }

        public int StudentId { get; set; }

        public int RegistrationTimeId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên nhóm")]
        [DisplayName("Tên nhóm của bạn")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [DisplayName("Email sinh viên thứ 2")]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}