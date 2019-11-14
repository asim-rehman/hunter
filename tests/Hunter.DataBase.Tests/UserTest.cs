using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Hunter.DataBase.Tests
{
    class UserTest : BaseTest
    {

        Guid addUser = Guid.Parse("7a32751b-892d-43d6-a569-2de6093b3d15");
        Guid updateUser = Guid.Parse("1ac9dc32-7b63-4660-8c50-d8be32714918");
        Guid deleteUser = Guid.Parse("ed6f6d33-9a5a-4884-925a-18545e91f343");


        string emailAdd = "admin@localhost.com";
        string emailUpdate = " update@localhost.co.uk    ";
        string emailDelete = "  delete@localhost.co.uk    ";

        string password1 = "test";
        string password2 = "test";

        [Test]
        public void Add()
        {
            User userAdd = new User
            {
                Id=addUser,
                FirstName = "admin",
                LastName = "admin",
                Username = emailAdd
            };
            User userUpdate = new User
            {
                Id=updateUser,
                FirstName = "Update",
                LastName = "User",
                Username = emailUpdate
            };
            User userDelete = new User
            {
                Id=deleteUser,
                FirstName = "Delete",
                LastName = "User",
                Username = emailDelete
            };


            UserRepository.Add(userAdd, password1);
            UserRepository.Add(userUpdate, password2);
            UserRepository.Add(userDelete, password1);

            int changes = UserRepository.SaveChanges();
            Assert.Greater(changes, 0);

        }

        [Test]
        public void InvalidEmail()
        {
            User invalidEmail = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Invalid",
                LastName = "Email",
                Username = "ajsdnajdn"
            };            
            Assert.Throws<ArgumentException>(delegate { UserRepository.Add(invalidEmail, password1); }, "Invalid email address", null);
        }

        [Test]
        public void DuplicateEmail()
        {
            User userDuplicate = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "admin",
                LastName = "admin",
                Username = emailAdd
            };

            UserRepository.Add(userDuplicate, password1);            
            Assert.Throws<DbUpdateException>(delegate { UserRepository.SaveChanges(); }, "Duplicate email address", null);
        }

        [Test]
        public void ChangePassword()
        {
            User user = UserRepository.RetrieveByPK(addUser);
            bool success = UserRepository.ChangePassword(user, password1, password2);
            UserRepository.SaveChanges();

            Assert.IsTrue(success);
        }

        [Test]
        public void Update()
        {
            User user = UserRepository.RetrieveByPK(updateUser);
            user.FirstName = "Update2";
            user.LastName = "update3";
            user.Username = " update1@localhost.com ";

            UserRepository.Update(user);
            Assert.Greater(UserRepository.SaveChanges(), 0);
        }

        [Test]
        public void UpdateWithInvalidEmail()
        {
            User user = UserRepository.RetrieveByPK(updateUser);
            user.FirstName = "Update2";
            user.LastName = "update3";
            user.Username = "update1localhost.com";

  
            Assert.Throws<ArgumentException>(delegate { UserRepository.Update(user); }, "Invalid email address", null);
        }


        [Test]
        public void UpdateWithDuplicateEmail()
        {
            User user = UserRepository.RetrieveByPK(updateUser);
            user.FirstName = "Update2";
            user.LastName = "update3";
            user.Username = emailAdd;

            UserRepository.Update(user);
            Assert.Throws<DbUpdateException>(delegate { UserRepository.SaveChanges(); }, "Duplicate email address", null);
        }
    }
}
