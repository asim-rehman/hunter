using Hunter.Mobile.Enums;
using System;
using System.Collections.Generic;

namespace Hunter.Mobile.Models
{
    public class Tasks : BaseModel
    {
        public Tasks()
        {
            TaskData = new HashSet<TaskData>();
        }
        public TaskType TaskType { get; set; }
        public Status Status { get; set; }
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime? End { get; set; }
        public DateTime? NextRun { get; set; } = DateTime.UtcNow;
        public DateTime? LastRun { get; set; }
        public int IntervalSeconds { get; set; }
        public int IntervalDays { get; set; }
        public bool IsEnabled { get; set; }
        public Guid DeviceId { get; set; }
        public Devices Device { get; set; }
        public ICollection<TaskData> TaskData { get; set; }
    }
}
