using System.Collections.Generic;
using superVise.Entities;
using superVise.Models.Requests;
using superVise.Models.Responses;

namespace superVise.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}