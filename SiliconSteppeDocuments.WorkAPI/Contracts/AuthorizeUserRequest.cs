namespace SiliconSteppeDocuments.API.Contracts
{
    public class AuthorizeUserRequest
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}