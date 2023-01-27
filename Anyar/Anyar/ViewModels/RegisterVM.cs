using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
