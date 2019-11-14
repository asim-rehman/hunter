using Hunter.DataBase.Interfaces;
using System;
namespace Hunter.DataBase.Models
{
    public class BaseModel : IKey<Guid>, IDateProperties, IDeleted
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}
