using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
            private IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("login")]
            public ActionResult Login(UserForLoginDto userForLoginDto)
            {
            if (userForLoginDto.Password == null || userForLoginDto.Email == null)
            {
                return BadRequest("Kullanici adi veya sifre alani bos");
            }    
                var userToLogin = _authService.Login(userForLoginDto);
                if (userToLogin == null)
                {
                    return BadRequest("Girisilemedi");
                }

                var result = _authService.CreateAccessToken(userToLogin);
                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Kullanıcı Adı Veya Şifre Yanlış");
            }

            [HttpPost("register")]
            public ActionResult Register(UserForRegisterDto userForRegisterDto)
            {
                var userExists = _authService.UserExists(userForRegisterDto.Email);
                if (!userExists)
                {
                    return BadRequest("Girisilemedi");
                }

                var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
                var result = _authService.CreateAccessToken(registerResult);
                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("asdsa");
            }
        }
    
}
