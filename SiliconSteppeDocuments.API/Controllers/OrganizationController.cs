using Microsoft.AspNetCore.Mvc;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.API.Models.Organization;
using SiliconSteppeDocuments.DB;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly SteppeContext _context;

        public OrganizationController(SteppeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<GetOrganizationsResponse> GetAll(GetOrganizationsRequest request)
        {
            var organizationModel = new OrganizationModel(_context);
            return await organizationModel.GetAllAsync(request);
        }
    }
}