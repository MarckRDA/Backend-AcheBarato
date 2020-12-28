using System;
using Domain.Crypt;
using Domain.Models.Crypt;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Users
{
    [ApiController]

    [Route("Brasileirao2020/[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IUserRepository _userRepository;

        [HttpGet("{idUser}")]
        public User GetUsuario(Guid idUser)
        {
            return _userServices.ObterUsuario(idUser);
        }

        [HttpPost("Login")]
        public ActionResult<dynamic> Authenticate(UserRequest request)
        {
            StringValues usuarioId;
            
            var md5 = new Crypt();

            if (!Request.Headers.TryGetValue("UserId", out usuarioId))
            {
                return Unauthorized();
            }
            var userGot = _userServices.ObterUsuario(Guid.Parse(usuarioId));
            if (userGot.Name != request.Name || !md5.ComparaMD5(request.Password, userGot.Password))
            {
              return Unauthorized();
            }

            if (userGot == null)
            {
               return Unauthorized(); 
            }

            var token = TokenServices.GerarToken(userGot);

            return new
            {
                usuario = userGot,
                token = token
            };
        }

 
        [HttpPost]
        public IActionResult PostUsuario(UserRequest request)
        {
            var md5 = new Crypt();
            var senhaCriptografada = md5.RetornarMD5(request.Password);
            var usuarioAAdicionar = _userServices.AdicionarUsuario(request.Name, senhaCriptografada, request.Email );

            if (!usuarioAAdicionar.isValid)
            {
                return Unauthorized();
            }

            return Ok(usuarioAAdicionar.id);
        }

     }
}