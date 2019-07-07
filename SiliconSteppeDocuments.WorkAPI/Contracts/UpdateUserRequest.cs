namespace SiliconSteppeDocuments.API.Contracts
{
    public class UpdateUserRequest
    {
        public long? ID { get; set; }
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

        public long DepartmentID { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
        public decimal Rate { get; set; }
    }
}
