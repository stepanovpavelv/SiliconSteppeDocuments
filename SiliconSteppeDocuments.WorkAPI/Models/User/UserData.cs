using SiliconSteppeDocuments.API.Common;
using SiliconSteppeDocuments.Model;

namespace SiliconSteppeDocuments.API.Models.User
{
    public class UserData : ResultQueryInfo
    {
        public bool IsNewUser { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public bool CanLogin { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 0 - OrganizationEmployee, 1 - Freelancer
        /// </summary>
        public short Type { get; set; }

        public Department Department { get; set; }

        public string Token { get; set; }

        public string Login { get; set; }
    }
}