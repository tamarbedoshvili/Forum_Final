using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Final.Dto;
using Final.Enum;
using Final.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserService _userService;
        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {

            var user = await _userService.GetUser(loginRequest.Username, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            if (user.IsBanned)
            {
                throw new Exception("Banned User");
            }
            bool isAdmin = user.Role == EUserRole.Administrator;

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
    };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("UserId", user.Id));
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials

            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AddUserDto addUserDto)
        {
            await _userService.AddUser(addUserDto);


            return Ok("User Created Successfully");
        }

    }
}

