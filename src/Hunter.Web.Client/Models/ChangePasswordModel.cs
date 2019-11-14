using Hunter.Web.Client.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hunter.Web.Client.Models
{
    public class ChangePasswordModel
    {
        public User User { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
