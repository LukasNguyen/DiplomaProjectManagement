using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaProjectManagement.Model.Enums;

namespace DiplomaProjectManagement.Model.Models
{
    [Table("RegistrationTimes")]
    public class RegistrationTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }

        public DateTime ClosedRegisteredDate { get; set; }

        public DateTime ClosedDate { get; set; }

        public virtual ICollection<DiplomaProjectRegistration> DiplomaProjectRegistration { get; set; }

        [DefaultValue(RegistrationStatus.Opening)]
        public RegistrationStatus RegistrationStatus { get; set; }
    }
}