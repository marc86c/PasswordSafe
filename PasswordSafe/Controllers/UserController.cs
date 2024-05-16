using Microsoft.AspNetCore.Mvc;
using PasswordSafe.Service;
using PasswordSafeCommon.Model;

namespace PasswordSafe.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private IUserService UserService;
        public UserController(IUserService userService)
        { 
            UserService = userService;
        }

        [HttpGet("User/{username}", Name = "User")]
        public ActionResult<User> Register(string username)
        {
            return Ok(UserService.GetUserData(username));
        }
    }
}
