using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("RegistrationTimes")]
    public class RegistrationTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }

        public DateTime FinishedDate { get; set; }
        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }
    }
}