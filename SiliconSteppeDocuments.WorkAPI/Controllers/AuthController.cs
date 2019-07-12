using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiliconSteppeDocuments.API.Models.User;
using SiliconSteppeDocuments.API.ViewModels;
using SiliconSteppeDocuments.DB;
using SiliconSteppeDocuments.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SteppeContext _steppeContext;

        public AuthController(UserManager<ApplicationUser> userManager, SteppeContext steppeContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _steppeContext = steppeContext;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                var userModel = new UserModel(_steppeContext);
                await userModel.AddUserClaimsAsync(_userManager, user, viewModel);

                await userModel.AddRoleToUserAsync(_userManager, user, UserModel.GetIdentityUserRoleByUserType(viewModel.Type));
            }

            return Ok(new { user.UserName });
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> CheckUser([FromBody] LoginViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, viewModel.Password))
            {
                var claim = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
                int expireInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInMinutes"]);

                var now = DateTime.Now;
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Site"],
                    audience: _configuration["Jwt:Site"],
                    notBefore: now,
                    claims: claim,
                    expires: now.Add(TimeSpan.FromMinutes(expireInMinutes)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

                return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        }
                    );
            }
            return Unauthorized();
        }
    }
}