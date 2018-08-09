using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SuperHero.Models
{
    public class LoginViewModeltest
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Required field!")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required field!")]
        [Display(Name = "Password")]
        public string PassWord { get; set; }  
        
    }
}