using DiplomaProjectManagement.Model.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("Facilities")]
    public class Facility : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public string LocationBuilding { get; set; }

        public string Email { get; set; }

        public string UrlWebsite { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}