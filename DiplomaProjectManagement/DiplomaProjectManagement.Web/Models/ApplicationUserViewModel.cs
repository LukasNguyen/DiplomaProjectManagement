using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomaProjectManagement.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        [Required]

        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}