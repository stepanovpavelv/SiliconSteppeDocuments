using SiliconSteppeDocuments.Model;
using System.Collections.Generic;
using System.Linq;

namespace SiliconSteppeDocuments.DB
{
    public class SteppeInitializer
    {
        public static void Initialize(SteppeContext context)
        {
            context.Database.EnsureCreated();

            CreateOrganizationTypes(context);

            CreateOrganizations(context);

            CreateDepartments(context);
        }
        
        private static void CreateOrganizationTypes(SteppeContext context)
        {
            if (context.OrganizationTypes.Any())
                return;

            var items = new List<OrganizationType>
            {
                new OrganizationType { Name = "Общепит" }
            };
            context.OrganizationTypes.AddRange(items);
            context.SaveChanges();
        }

        private static void CreateOrganizations(SteppeContext context)
        {
            if (context.Organizations.Any())
                return;

            var organizationType = context.OrganizationTypes.FirstOrDefault(x => x.Name == "Общепит");

            var items = new List<Organization>
            {
                new Organization { OrganizationTypeID = organizationType.ID, INN = "12365127362", Name = "Steppe Cafe", FullName = "OOO Steppe Cafe" }
            };
            context.Organizations.AddRange(items);
            context.SaveChanges();
        }

        private static void CreateDepartments(SteppeContext context)
        {
            if (context.Departments.Any())
                return;

            var organization = context.Organizations.FirstOrDefault(x => x.Name == "Steppe Cafe");

            var items = new List<Department>
            {
                new Department { Name = "Кухня", OrganizationID = organization.ID }
            };
            context.Departments.AddRange(items);
            context.SaveChanges();
        }
    }
}