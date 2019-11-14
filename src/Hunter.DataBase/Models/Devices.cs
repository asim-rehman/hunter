using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hunter.DataBase.Models
{
    public class Devices : BaseModel
    {
        public Devices()
        {
            Tasks = new HashSet<Tasks>();
        }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }      
        public User User { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
