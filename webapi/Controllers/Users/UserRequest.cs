
using Domain.Models.Users;

namespace webapi.Controllers.Users
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Profile Profile {get; set;}
    }
}