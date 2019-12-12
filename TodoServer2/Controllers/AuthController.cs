using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TodoServer2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public AuthController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Login login)
        {
            try
            {
                if(login == null)
                {
                    return BadRequest("Invalid client request, login object was empty");
                }

                var user = _repository.User.GetUserByUsername(login.Username);

                if (user == null)
                {
                    _logger.LogError($"User with username {login.Username} was not found in database");
                    return Unauthorized("Invalid client request, user name does not exist in database");
                }

                if (user.Username.Equals(login.Username) && user.Password.Equals(login.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("verySecretKeyHiddenWell@748"));

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:44341",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }

                else
                {
                    return Unauthorized("Invalid client request, login details doesn't match");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}