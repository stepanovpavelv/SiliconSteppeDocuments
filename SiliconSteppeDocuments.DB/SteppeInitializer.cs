using SiliconSteppeDocuments.Common.Helpers;
using SiliconSteppeDocuments.Model;
using System;
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

            CreateUsers(context);
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

        private static void CreateUsers(SteppeContext context)
        {
            if (context.Users.Any())
                return;

            var department = context.Departments.FirstOrDefault(x => x.Name == "Кухня");

            var items = new List<User>
            {
                new User
                {
                    CanLogin = true,
                    DepartmentID = department.ID,
                    Description = "Тестовый админ",
                    Login = "admin",
                    FirstName = "Петр (админ)",
                    SecondName = "Сидоров",
                    MiddleName = "Алексеевич",
                    Type = UserType.OrganizationEmployee,
                    Token = Md5Helper.GetMd5Hash("admin"),
                    Password = Base64Helper.Base64Encode("admin")
                },
                new User
                {
                    CanLogin = true,
                    Description = "Лучший в своем деле!",
                    Login  = "free",
                    FirstName = "Иван",
                    SecondName = "Иванов",
                    MiddleName = "Иванович", 
                    Type = UserType.Consultant,
                    Token = Md5Helper.GetMd5Hash("free"),
                    Password = Base64Helper.Base64Encode("free")
                }
            };

            context.Users.AddRange(items);
            context.SaveChanges();
        }
    }
}