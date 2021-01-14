using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers.Users
{
    [ApiController]
    [Route("achebarato/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostUser(UserRequest request)
        {
            return Ok();
        }
    }
}