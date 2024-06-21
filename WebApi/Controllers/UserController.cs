using Final.Dto;
using Final.Entities;
using Final.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("User")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            await _userService.AddUser(addUserDto);
            return Ok();
        }

        [HttpDelete("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> DeleteUser(string email)
        {
            await _userService.DeleteUser(email);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUser(updateUserDto);
            return Ok();
        }

        [HttpGet("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> GetUser(string email)
        {
            return Ok(await _userService.GetUser(email));
        }

        [HttpPost("ban-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> BanUser(BanUserDto banUserDto)
        {
            await _userService.BanUser(banUserDto);
            return Ok();
        }

        [HttpPost("unban-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> UnbanUser(BanUserDto banUserDto)
        {
            await _userService.UnBanUser(banUserDto);
            return Ok();
        }
    }
}
