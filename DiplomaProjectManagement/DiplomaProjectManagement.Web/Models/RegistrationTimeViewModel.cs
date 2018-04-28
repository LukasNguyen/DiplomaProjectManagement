using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaProjectManagement.Model.Enums;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Web.Models
{
    public class RegistrationTimeViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đợt đăng ký.")]
        [MaxLength(250)]
        public string Name { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime TeacherAssignGradesDate { get; set; }

        public DateTime ClosedDate { get; set; }

        [DefaultValue(RegistrationStatus.Opening)]
        public RegistrationStatus RegistrationStatus { get; set; }
    }
}
