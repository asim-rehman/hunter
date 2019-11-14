using System;

namespace Hunter.DataBase
{
    /// <summary>
    /// Used for duplicate items
    /// </summary>
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException()
        {
        }

        public DuplicateItemException(string message)
            : base(message)
        {
        }

        public DuplicateItemException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
