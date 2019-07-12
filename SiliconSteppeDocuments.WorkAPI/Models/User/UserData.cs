using SiliconSteppeDocuments.API.Common;
using SiliconSteppeDocuments.API.Models.Organization;

namespace SiliconSteppeDocuments.API.Models.User
{
    public class UserData : ResultQueryInfo
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public bool CanLogin { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 0 - OrganizationEmployee, 1 - Freelancer
        /// </summary>
        public short Type { get; set; }
        public DepartmentData Department { get; set; }

        public UserData() { }

        public UserData(UserData clone)
        {
            if (clone == null)
                return;

            Login = clone.Login;
            FirstName = clone.FirstName;
            SecondName = clone.SecondName;
            MiddleName = clone.MiddleName;
            CanLogin = clone.CanLogin;
            Description = clone.Description;
            Type = clone.Type;
            Department = clone.Department;
        }
    }
}