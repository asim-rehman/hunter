using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using System;

namespace Hunter.DataBase.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User Add(User user, string password);
        void Update(User user);
        bool ChangePassword(User Entity, string currentPassword, string newPassword);
        bool Exists(string username);
        User Authenticate(string username, string password);
    }
}
