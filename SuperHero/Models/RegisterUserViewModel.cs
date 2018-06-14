using System.ComponentModel.DataAnnotations;

namespace SuperHero.Models
{
    public class RegisterUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage = "A jelszónak minimun 4 karakterből kell állnia!")]
        [MaxLength(30,ErrorMessage = "A jelszónak maximum 30 karakterből kell állnia!")]
        public string PassWord { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "A jelszónak minimun 4 karakterből kell állnia!")]
        [MaxLength(30, ErrorMessage = "A jelszónak maximum 30 karakterből kell állnia!")]
        public string ConfirmPassWord { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Az email-t megfelelő formátumban kell megadni!")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Fullname")]
        public string Name { get; set; }
               
    }
}