using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("DiplomaProjectRegistrations")]
    public class DiplomaProjectRegistration
    {
        [Key]
        [Column(Order = 1)]
        public int DiplomaProjectId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int RegistrationTimeId { get; set; }

        [ForeignKey("DiplomaProjectId")]
        public virtual DiplomaProject DiplomaProject { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("RegistrationTimeId")]
        public virtual RegistrationTime RegistrationTime { get; set; }

        [DefaultValue(0)]
        public int limitedStudentRegister { get; set; }

        public bool isOpened { get; set; }
    }
}
