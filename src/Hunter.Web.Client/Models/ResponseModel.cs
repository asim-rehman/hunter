using System;

namespace Hunter.Web.Client.Models
{
    public class ResponseModel<T> where T : class
    {
        public ResponseModel()
        {
            //Exception = new Exception();
        }
        public string Status { get; set; }
        public string Content { get; set; }
        public T Entity { get; set; }
        public Exception Exception { get; set; }
    }
}
