using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProjectManagement.Web.Models
{
    public class DiplomaProjectRegistrationViewModel
    {
        public int DiplomaProjectId { get; set; }

        public int StudentId { get; set; }

        public int RegistrationTimeId { get; set; }

        public float? IntroducedGrades { get; set; }

        public float? ReviewedGrades { get; set; }
    }
}
