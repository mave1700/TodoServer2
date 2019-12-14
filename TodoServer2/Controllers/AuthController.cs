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
                if(login.Username == null || login.Password == null)
                {
                    return BadRequest("Invalid client request, login credentials are missing");
                }

                var user = _repository.User.GetUser(login.Username);

                if (user == null)
                {
                    _logger.LogError($"User with username {login.Username} was not found in database");
                    return Unauthorized("Login credentials was wrong, access denied");
                }

                if (user.Username.Equals(login.Username) && user.Password.Equals(login.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("verySecretKeyHiddenWell@748"));
                    var claimsList = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:44341",
                        audience: "https://localhost:44341",
                        claims: claimsList,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }

                else
                {
                    return Unauthorized("Login credentials was wrong, access denied");
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