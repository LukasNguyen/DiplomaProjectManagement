using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public float? IntroducedGrades { get; set; }

        public float? ReviewedGrades { get; set; }

        public string TeamName { get; set; }
    }
}