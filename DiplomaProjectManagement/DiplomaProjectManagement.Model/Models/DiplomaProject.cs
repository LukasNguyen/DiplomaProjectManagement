using DiplomaProjectManagement.Model.Abstracts;
using System.Collections.Generic;
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

        public bool IsDisplayed { get; set; }

        public int LecturerId { get; set; }

        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }
    }
}