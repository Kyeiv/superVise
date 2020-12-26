using System.Collections.Generic;
using System.Linq;
using superVise.Entities;
using superVise.Entities.Context;
using superVise.Helpers.Exceptions;
using superVise.Models.Requests;
using superVise.Services.Interfaces;

namespace superVise.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(AuthenticateRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;
            
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // return null if user not found
            if (user == null) return null;

            return !BCrypt.Net.BCrypt.Verify(model.Password, user.Password) ? null : user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }
        
        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            
            if (user.IsAdmin.ToUpper() != "FALSE" && user.IsAdmin.ToUpper() != "TRUE")
                throw new AppException("IsAdmin \"" + user.IsAdmin + "\" bad value");

            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");

                user.Username = userParam.Username;
            }

            if (userParam.IsAdmin.ToUpper() != "FALSE" && userParam.IsAdmin.ToUpper() != "TRUE")
                throw new AppException("IsAdmin \"" + user.IsAdmin + "\" bad value");
            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.IsAdmin))
                user.IsAdmin = userParam.IsAdmin;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}