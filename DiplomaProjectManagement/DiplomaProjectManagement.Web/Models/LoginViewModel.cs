using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomaProjectManagement.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please type your username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please type your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}