using System;
using Domain.Crypt;
using Domain.Models.Crypt;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
            StringValues usuarioId;
            var md5 = new Crypt();

            if (!Request.Headers.TryGetValue("UserId", out usuarioId))
            {
                return Unauthorized();
            }

            var getedUSer = userservices.GetUser(Guid.Parse(usuarioId));

            if (getedUSer.Email != request.Email || !md5.ComparaMD5(request.Password, getedUSer.Password))
            {
                return Unauthorized();
            }

            if (getedUSer == null)
            {
               return Unauthorized(); 
            }

            var token = TokenServices.GerarToken(getedUSer);

            return new
            {
                usuario = getedUSer,
                token = token
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