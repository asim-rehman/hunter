using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository.Interfaces;
using System;

namespace Hunter.DataBase.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private IHunterDBContext dBContext = null;
        string invalidEmail = "Invalid email address";
        public UserRepository(IHunterDBContext hunterDBContext) : base(hunterDBContext)
        {
            dBContext = hunterDBContext;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = RetrieveByQuery(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        
        public User Add(User Entity, string password)
        {

            if (!IsValidEmailAddress(Entity.Username))
                throw new ArgumentException(invalidEmail, "UserName", null);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            Entity.PasswordHash = passwordHash;
            Entity.PasswordSalt = passwordSalt;
            Entity.Username = Entity.Username.Trim();
            base.Add(Entity);

            return Entity;
        }

        public override void Update(User Entity)
        {
            if (!IsValidEmailAddress(Entity.Username))
                throw new ArgumentException(invalidEmail, "UserName", null);

            var user = RetrieveByPK(Entity.Id);

            user.Username = Entity.Username.Trim();
            user.FirstName = Entity.FirstName;
            user.LastName = Entity.LastName;
            user.DateModified = DateTime.UtcNow;

            base.Update(user);
        }

        public bool ChangePassword(User Entity, string currentPassword, string newPassword)
        {

            var user = RetrieveByPK(Entity.Id);

            if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) &&
                VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                base.Update(user);
                return true;
            }
      
            return false;
        }

        /// <summary>
        /// This method creates a SHA512 hash of the passwod along with the salt
        /// </summary>
        /// <param name="password">The password to encrypt</param>
        /// <param name="passwordHash">Returns passwordhash</param>
        /// <param name="passwordSalt">Returns passwordsalt</param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        /// <summary>
        /// Verifies a user's password
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <param name="storedHash">The stored password hash</param>
        /// <param name="storedSalt">The stored password salt</param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations
                                .EmailAddressAttribute()
                                .IsValid(emailAddress);
        }

        public bool Exists(string username)
        {
            return RetrieveByQuery(e => e.Username == username.Trim()) != null;
        }

    }
}
