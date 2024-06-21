using Final.Domain.Dto;
using Final.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> GetUsers()
        {

            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("ChangeUserRole")]

        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleDto changeUserRoleDto)
        {

            await _userService.ChangeUserRole(changeUserRoleDto);
            return Ok();
        }


    }
}
