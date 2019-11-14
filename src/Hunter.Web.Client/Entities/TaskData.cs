using System;

namespace Hunter.Web.Client.Models.Entities
{
    public class TaskData : BaseModel
    {
        public string Data { get; set; }
        public Guid TasksId { get; set; }
        public Tasks Tasks { get; set; }       
    }
}
