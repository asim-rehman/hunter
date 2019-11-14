using System;

namespace Hunter.Mobile.Models
{
    public class TaskData : BaseModel
    {
        public string Data { get; set; }
        public Guid TasksId { get; set; }
        public Tasks Tasks { get; set; }       
    }
}
