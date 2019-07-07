using Microsoft.EntityFrameworkCore;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.DB;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Models.Organization
{
    public class OrganizationModel
    {
        private readonly SteppeContext _context;

        public OrganizationModel(SteppeContext context)
        {
            _context = context;
        }

        public async Task<GetOrganizationsResponse> GetAllAsync(GetOrganizationsRequest request)
        {
            var items = await _context.Organizations.ToListAsync();
            return new GetOrganizationsResponse
            {
                Items = items
            };
        }
    }
}
