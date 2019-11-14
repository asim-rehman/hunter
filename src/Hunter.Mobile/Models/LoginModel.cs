namespace Hunter.Mobile.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                    return true;
                else
                    return false;
            }

        }
    }
}