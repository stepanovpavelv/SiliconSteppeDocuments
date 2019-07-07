using SiliconSteppeDocuments.API.Common;
using SiliconSteppeDocuments.Model;
using System.Collections.Generic;

namespace SiliconSteppeDocuments.API.Contracts
{
    public class GetOrganizationsResponse : ResultQueryInfo
    {
        public List<Organization> Items { get; set; }
    }
}