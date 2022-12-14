using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sat.Recruitment.Contracts.Engine;
using Sat.Recruitment.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserEngine _userService;
        private readonly IValidator<Models.User> _userValidator;
        private readonly IValidator<int> _integerValidator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserEngine userService,
            IValidator<Models.User> userValidator,
            IValidator<int> integerValidator,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _userValidator = userValidator;
            _integerValidator = integerValidator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            var resultValidator = _userValidator.Validate(newUser);
            
            if (!resultValidator.IsValid)
            {
                return BadRequest(string.Join(", ", resultValidator.Errors));
            }
            try
            {         
                var created = await _userService.AddUser(newUser);
                
                if(created == null)
                {
                    return BadRequest("The user can be created");
                }
                else if(created.Id != 0)
                {
                    return StatusCode(StatusCodes.Status201Created, created);
                }
                else 
                {
                    return BadRequest("The user is duplicated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create user error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("/update-user")]
        public async Task<IActionResult> UpdateUser(User newUser)
        {
            var resultValidator = _userValidator.Validate(newUser);

            if (!resultValidator.IsValid)
            {
                return BadRequest(string.Join(", ", resultValidator.Errors));
            }
            try
            {
                var updated = await _userService.UpdateUser(newUser);
                if (updated == null)
                {
                    return BadRequest("The user can be updated");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, updated);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update user error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var listUsers = await _userService.GetAll();
                return StatusCode(StatusCodes.Status200OK, listUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/get-user-by-Id/{Id:int}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            try
            {
                var listUsers = await _userService.GetById(Id);
                return StatusCode(StatusCodes.Status200OK, listUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("/delete-user/{Id:int}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var resultValidator = _integerValidator.Validate(Id);

            if (!resultValidator.IsValid)
            {
                return BadRequest(string.Join(", ", resultValidator.Errors));
            }
            try
            {
                var created = await _userService.Delete(Id);

                if (!created)
                {
                    return BadRequest("The user can be deleted");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete user error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }

}
