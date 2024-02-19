using DatingApp.API.Databases.Entities;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetUserById(id);
            if (user is null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
           _userService.CreateUser(user);
           return Ok(); 
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser is null) return NotFound("User not found");
            _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleteUser = _userService.GetUserById(id);
            if (deleteUser is null) return NotFound("User not found");
            _userService.DeleteUser(deleteUser);
            return Ok();
        }
    }
}