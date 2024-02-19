using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Databases.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthRegisterDto registerUser)
        {
            registerUser.Username = registerUser.Username.ToLower();
            if (_userService.GetUserByUsername(registerUser.Username) is not null) 
                return BadRequest("Username already registered");
            
            using var hashFunc = new HMACSHA256();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUser.Password);
            
            var newUser = new User () {
                Username = registerUser.Username,
                Email = registerUser.Username,
                PasswordHash = hashFunc.ComputeHash(passwordBytes),
                PasswordSalt = hashFunc.Key,
            };
            _userService.CreateUser(newUser);
            // Generate JWT token
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthLoginDto loginUser)
        {
            loginUser.Username = loginUser.Username.ToLower();

            var existedUser = _userService.GetUserByUsername(loginUser.Username);
            if (existedUser is null) return Unauthorized("User not found");

            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                    return Unauthorized("Password not match");
            }
            // Generate JWT token
            return Ok();
        }
    }
}