using System.Collections.Generic;

namespace Hunter.DataBase.Models
{
    public class User : BaseModel
    {
        public User()
        {
            Devices = new HashSet<Devices>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Devices> Devices { get; set; }
    }
}
