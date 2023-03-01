using System.ComponentModel.DataAnnotations;

namespace Autoparts.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set; } = false;
    }
}
