using System;

namespace Hunter.DataBase.Interfaces
{
    interface IDateProperties
    {
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
    }
}
