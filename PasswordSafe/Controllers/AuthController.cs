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
        public AuthController(IAuthService authService) 
        {
            AuthService = authService;
        }

        [HttpPost("Register", Name = "Register")]
        public ActionResult<bool> Register([FromBody]RegisterModel registerModel)
        {
            return Ok(AuthService.Register(registerModel.Username, registerModel.Password));
        }

        [HttpPost("Login", Name = "Login")]
        public ActionResult<bool> Login(LoginModel loginModel)
        {
            return Ok(AuthService.Login(loginModel.Username, loginModel.Password));
        }
    }
}
