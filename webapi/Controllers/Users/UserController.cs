using System;

using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using webapi.Crypts;

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

            var userDTO = new UserDTO()
            {
                UserId = getedUSer.Id,
                Name = getedUSer.Name,
                SearchTag = getedUSer.SearchTags,
                WishListProducts = getedUSer.WishListProducts,
            };

            return new
            {
                token = token.ToString(),
                user = userDTO
            };
        }

        [HttpPost("alarmsprice")]
        public IActionResult PostAlarmPrice(AlarmPriceRequest request)
        {
            StringValues userId;

            if (!Request.Headers.TryGetValue("userid", out userId))
            {
                return Unauthorized();
            }

            if (!userservices.UpdateAlarmPriceProductInformations(Guid.Parse(userId), request.ProductId, request.PriceToMonitor))
            {
                return Unauthorized();
            }

            return NoContent();
        }

        [HttpPost("addPreferences")]
        public IActionResult PostUserPreferences(UserPreferencesRequest request)
        {
            StringValues userId;

            if (!Request.Headers.TryGetValue("userid", out userId))
            {
                return Unauthorized();
            }

            if (!userservices.AddSearchTagInUserPreferences(Guid.Parse(userId), request.SearchTag))
            {
                return Unauthorized();
            }

            return NoContent();

        }

        [HttpPost]
        public IActionResult PostUsuario(UserRequest request)
        {
            var md5 = new Crypt();
            //recebe o password encriptografado
            var cryptoPassword = md5.RetornarMD5(request.Password);

            var userAdded = userservices.CreateUser(request.Name, cryptoPassword, request.Email, Profile.Client, request.PhoneNumber);

            if (userAdded == null)
            {
                return Unauthorized("Este email já está cadastrado!");
            }

            if (!userAdded.Validate().isValid)
            {
                return Unauthorized();
            }

            return Ok();
        }

    }

}