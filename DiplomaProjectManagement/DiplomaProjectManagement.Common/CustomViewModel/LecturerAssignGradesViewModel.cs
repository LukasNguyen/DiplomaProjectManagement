using DiplomaProjectManagement.Model.Enums;
using System.ComponentModel;

namespace DiplomaProjectManagement.Common.CustomViewModel
{
    public class LecturerAssignGradesViewModel
    {
        [DisplayName("Mã sinh viên")]
        public int StudentId { get; set; }

        [DisplayName("Tên sinh viên")]
        public string StudentName { get; set; }

        [DisplayName("Điểm hướng dẫn")]
        public float? IntroducedGrades { get; set; }

        [DisplayName("Điểm đánh giá")]
        public float? ReviewedGrades { get; set; }

        [DisplayName("Tên đồ án")]
        public string DiplomaProjectName { get; set; }

        public RegistrationStatus RegistrationStatus { get; set; }

        public int RegistrationTimeId { get; set; }

        public int DiplomaProjectId { get; set; }

        public string TeamName { get; set; }
    }
}