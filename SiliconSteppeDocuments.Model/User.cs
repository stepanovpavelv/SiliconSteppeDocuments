using System.Collections.Generic;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class User
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool CanLogin { get; set; }

        public UserType Type { get; set; }

        public string Description { get; set; }

        public Department Department { get; set; }

        public long? DepartmentID { get; set; }

        public string Token { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
