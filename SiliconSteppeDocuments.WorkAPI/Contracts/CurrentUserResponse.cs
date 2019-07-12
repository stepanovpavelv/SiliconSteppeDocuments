using SiliconSteppeDocuments.API.Models.User;

namespace SiliconSteppeDocuments.API.Contracts
{
    public class CurrentUserResponse : UserData
    {
        public CurrentUserResponse()
        {
        }

        public CurrentUserResponse(UserData userData) : base(userData)
        {
        }
    }
}