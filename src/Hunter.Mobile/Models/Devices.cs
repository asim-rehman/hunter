using System.Collections.Generic;

namespace Hunter.Mobile.Models
{
    public class Devices : BaseModel
    {
        public Devices()
        {
            Tasks = new HashSet<Tasks>();
        }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }

}