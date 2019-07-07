using Microsoft.AspNetCore.Mvc;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.API.Models.User;
using SiliconSteppeDocuments.DB;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SteppeContext _context;

        public UserController(SteppeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var userModel = new UserModel(_context);
            return await userModel.UpdateUser(request);
        }

        [HttpPost]
        public async Task<AuthorizeUserResponse> Authorize(AuthorizeUserRequest request)
        {
            var userModel = new UserModel(_context);
            return await userModel.GetUser(request);
        }
    }
}