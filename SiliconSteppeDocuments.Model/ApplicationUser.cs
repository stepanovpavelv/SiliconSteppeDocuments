using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SiliconSteppeDocuments.Model
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class ApplicationUser : IdentityUser<long>
    {
        public Department Department { get; set; }

        public long? DepartmentID { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }

    public class ApplicationRole : IdentityRole<long> { }
    public class UserRole : IdentityUserRole<long> { }
    public class UserClaim : IdentityUserClaim<long> { }
    public class UserLogin : IdentityUserLogin<long> { }
    public class UserToken : IdentityUserToken<long> { }
    public class UserRoleClaim : IdentityRoleClaim<long> { }
}