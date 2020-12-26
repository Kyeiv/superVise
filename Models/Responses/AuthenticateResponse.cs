using superVise.Entities;

namespace superVise.Models.Responses
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string IsAdmin { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            IsAdmin = user.IsAdmin;
            Username = user.Username;
            Token = token;
        }
    }
}