using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoServer2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
        public IActionResult GetUserWithDetails()
        {
            try
            {
                string userName = User.FindFirst(ClaimTypes.Name)?.Value;
                var user = _repository.User.GetUserWithDetails(userName);

                if (user == null)
                {
                    _logger.LogError($"User with username: {userName} was not found in database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user {userName}");

                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
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