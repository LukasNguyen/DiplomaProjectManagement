using System.ComponentModel;

namespace DiplomaProjectManagement.Common.CustomViewModel
{
    public class DiplomaProjectDetailViewModel
    {
        [DisplayName("Tên đồ án")]
        public string Name { get; set; }

        [DisplayName("Mô tả đồ án")]
        public string Description { get; set; }

        [DisplayName("Giảng viên hướng dẫn")]
        public string LecturerName { get; set; }

        [DisplayName("Điểm hướng dẫn")]
        public float? IntroducedGrades { get; set; }

        [DisplayName("Điểm đánh giá")]
        public float? ReviewedGrades { get; set; }
    }
}