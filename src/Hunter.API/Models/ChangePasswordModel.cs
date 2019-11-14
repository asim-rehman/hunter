using Hunter.DataBase.Models;

namespace Hunter.API.Models
{
    public class ChangePasswordModel
    {
        public User User { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
