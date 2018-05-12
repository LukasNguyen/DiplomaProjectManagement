using DiplomaProjectManagement.Model.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("Students")]
    public class Student : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public float? GPA { get; set; }

        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }
    }
}