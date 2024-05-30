using Microsoft.AspNetCore.Mvc;
using PasswordSafe.Service;
using PasswordSafeCommon.Model;

namespace PasswordSafe.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; set; }
        private IUserService UserService { get; set; }
        public AuthController(IAuthService authService, IUserService userService) 
        {
            AuthService = authService;
            UserService = userService;
        }

        [HttpPost("Register", Name = "Register")]
        public ActionResult<bool> Register([FromBody]RegisterModel registerModel)
        {
            return Ok(AuthService.Register(registerModel.Username, registerModel.Password));
        }

        [HttpPost("Login", Name = "Login")]
        public ActionResult<User> Login(LoginModel loginModel)
        {
            var isSucessful = AuthService.Login(loginModel.Username, loginModel.Password);
            if (!isSucessful)
            {
                return BadRequest();            
            }

            return Ok(UserService.GetUserData(loginModel.Username));
        }
    }
}
