using System;

namespace Hunter.Mobile.Models
{
    public class ResponseModel<T> where T : class
    {
        public string Status { get; set; }
        public string Content { get; set; }
        public T Entity { get; set; }
        public Exception Exception { get; set; }
    }
}