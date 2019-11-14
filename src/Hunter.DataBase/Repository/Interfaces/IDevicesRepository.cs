using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using System;

namespace Hunter.DataBase.Repository.Interfaces
{
    public interface IDevicesRepository : IRepository<Devices, Guid>, IExists<Guid>
    {

    }
}
