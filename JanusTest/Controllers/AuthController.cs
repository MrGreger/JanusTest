using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JanusTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public class LoginDTO
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Login);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = loginDTO.Login
                };

                var result = await _userManager.CreateAsync(user, loginDTO.Password);


                if (!result.Succeeded)
                {
                    return Ok(result);
                }
            }

            await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });

            return Ok();       
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult> Current()
        { 
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            return Ok(user);
        }
    }
}
