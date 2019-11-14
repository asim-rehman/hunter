using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hunter.DataBase.Repository
{
    /// <summary>
    /// Repo for performing CRUD against Devices
    /// </summary>
    public class DevicesRepository : BaseRepository<Devices>, IDevicesRepository
    {
        private IHunterDBContext dBContext = null;
        public DevicesRepository(IHunterDBContext hunterDBContext) : base(hunterDBContext)
        {
            dBContext = hunterDBContext;
        }

        public bool Exists(Guid id)
        {
            return RetrieveByPK(id) != null;
        }
        /// <summary>
        /// Retrieves all devices excepted deleted ones.
        /// </summary>
        /// <returns>Collection of devices</returns>
        public override IQueryable<Devices> RetrieveAll()
        {
            return dBContext.Devices.Include(p => p.Tasks).Where(p => p.IsDeleted == false);
        }
        /// <summary>
        /// Retrieve Devices including their related Tasks, overrides base method
        /// </summary>
        /// <param name="key">The Id of Device</param>
        /// <returns>Device or null if device does not exist</returns>
        public override Devices RetrieveByPK(Guid key)
        {
            return dBContext.Devices.Include(p => p.Tasks).Where(p => p.Id == key).FirstOrDefault();
        }
    }
}
