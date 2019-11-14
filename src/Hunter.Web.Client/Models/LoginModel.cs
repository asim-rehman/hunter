using System.ComponentModel.DataAnnotations;

namespace Hunter.Web.Client.Models
{
    public class LoginModel : BaseModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}
