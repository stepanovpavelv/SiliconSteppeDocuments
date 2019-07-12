using SiliconSteppeDocuments.Model;
using System.ComponentModel.DataAnnotations;

namespace SiliconSteppeDocuments.API.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(150)]
        public string FirstName { get; set; }
        [MaxLength(150)]
        public string SecondName { get; set; }
        [MaxLength(150)]
        public string MiddleName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserType Type { get; set; }
    }
}