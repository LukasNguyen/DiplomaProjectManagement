using System.Collections.Generic;
using System.ComponentModel;
using DiplomaProjectManagement.Model.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("DiplomaProjects")]
    public class DiplomaProject : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string Description { get; set; }

        [DefaultValue(0)]
        public int numberOfStudentsRegistered { get; set; }

        public int LecturerId { get; set; }

        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }
    }
}