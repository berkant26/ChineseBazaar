using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
                _userService = userService;
                _logger = logger;
        }
        [HttpPost]
        public void AddUser(User user)
        {
           _userService.Add(user);
        }
        
    }
}
