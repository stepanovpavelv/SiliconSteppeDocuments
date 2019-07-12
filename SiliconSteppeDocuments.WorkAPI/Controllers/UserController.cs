using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.API.Models.User;
using SiliconSteppeDocuments.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<CurrentUserResponse> CurrentUser()
        {
            var currentUserNameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByNameAsync(currentUserNameClaim.Value);
            if (currentUser == null)
            {
                var badResponse = new CurrentUserResponse();
                badResponse.AddMessage(Common.MessageType.Error, "User not found");
                return badResponse;
            }

            var userModel = new UserModel(null);
            var userInfo = await userModel.GetUserInfoAsync(_userManager, currentUser);

            return new CurrentUserResponse(userInfo);
        }
    }
}