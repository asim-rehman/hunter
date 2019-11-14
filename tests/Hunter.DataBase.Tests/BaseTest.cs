using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hunter.DataBase.Tests
{
    public class BaseTest
    {
        private DevicesRepository devicesRepository;
        private TasksRepository tasksRepository;
        private TasksDataRepository tasksDataRepository;
        private UserRepository userRepository;

        protected IHunterDBContext Connect
        {
            get
            {
                string connection = @"Server=.\SQLEXPRESS;Database=HunterDBTest;User Id=sa;password=asim;";
                var builder = new DbContextOptionsBuilder<HunterDBContext>().UseSqlServer(connection);
                builder.UseLazyLoadingProxies(false);
                return new HunterDBContext(builder.Options);
            }
        }
        protected DevicesRepository DeviceRepository
        {
            get
            {
                if (devicesRepository == null)
                {
                    devicesRepository = new DevicesRepository(Connect);
                }
                return devicesRepository;
            }
        }
        protected TasksRepository TasksRepository
        {
            get
            {
                if (tasksRepository == null)
                {
                    tasksRepository = new TasksRepository(Connect);
                }
                return tasksRepository;
            }
        }
        protected TasksDataRepository TasksDataRepository
        {
            get
            {
                if (tasksDataRepository == null)
                {
                    tasksDataRepository = new TasksDataRepository(Connect);
                }
                return tasksDataRepository;
            }
        }
        protected UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(Connect);
                }
                return userRepository;
            }
        }
    }
}
