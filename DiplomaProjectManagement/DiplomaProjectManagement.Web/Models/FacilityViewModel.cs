using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaProjectManagement.Web.Models
{
    public class FacilityViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khoa.")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vị trí khoa.")]
        [MaxLength(1, ErrorMessage = "Vị trí khoa chỉ gồm 1 ký tự.")]
        public string LocationBuilding { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Url.")]
        [Url]
        [MaxLength(250)]
        public string UrlWebsite { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}