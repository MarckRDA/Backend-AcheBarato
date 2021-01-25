using System;
using Domain.Crypt;
using Domain.Models.Crypt;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers.Users
{
    [ApiController]
    [Route("achebarato/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userservices;

        public UsersController(IUserService userservices)
        {
            this.userservices = userservices;
        }

        [HttpGet("{idUser}")]
        public User GetUser(Guid idUser)
        {
            return userservices.GetUser(idUser);
        }

        [HttpPost("login")]
        public ActionResult<dynamic> Authenticate(UserRequest request)
        {
            var md5 = new Crypt();
            var getedUSer = userservices.GetUserByEmail(request.Email);

            if (getedUSer == null)
            {
                return Unauthorized();
            }

            if (getedUSer.Email != request.Email || !md5.ComparaMD5(request.Password, getedUSer.Password))
            {
                return Unauthorized();
            }

            var token = TokenServices.GerarToken(getedUSer);

            return new
            { 
                token = token.ToString(),
                userId = getedUSer.Id 
            };
        }

        [HttpPost]
        public IActionResult PostUsuario(UserRequest request)
        {
            var md5 = new Crypt();
            //recebe o password encriptografado
            var cryptoPassword = md5.RetornarMD5(request.Password);
            var userAdded = userservices.CreateUser(request.Name, cryptoPassword, request.Email, Profile.Client);

            if (!userAdded.Validate().isValid)
            {
                return Unauthorized();
            }

            return Ok(userAdded.Id);
        }

    }

}