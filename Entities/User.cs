using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace superVise.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string IsAdmin { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}