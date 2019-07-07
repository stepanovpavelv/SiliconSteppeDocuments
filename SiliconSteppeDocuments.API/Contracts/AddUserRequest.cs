namespace SiliconSteppeDocuments.API.Contracts
{
    public class AddUserRequest
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public short Type { get; set; }
    }
}
