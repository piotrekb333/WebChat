using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Hasła się różnią !")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}