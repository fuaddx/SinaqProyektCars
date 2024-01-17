using System.ComponentModel.DataAnnotations;

namespace Cars.ViewModels.AuthVm
{
    public class RegisterVm
    {
        [Required(ErrorMessage ="Enter Name Correctly")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Surname Correctly")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter Username Correctly"),MaxLength(32)]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(PasswordConfirm))]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
