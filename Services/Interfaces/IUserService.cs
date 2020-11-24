using System.Collections.Generic;
using superVise.Entities;
using superVise.Models.Requests;
using superVise.Models.Responses;

namespace superVise.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}