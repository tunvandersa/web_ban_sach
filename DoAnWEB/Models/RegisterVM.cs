using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAnWEB.Models
{
    public class RegisterVM

    {
        [Key] public string ID { get; set; }
        [Required(ErrorMessage = "Username cannot be blank. ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be blank. ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password cannot be blank. ")]
        [Compare("Password", ErrorMessage = "Password and Confirmpassword do not match.")]
        public string ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
      

    }
}