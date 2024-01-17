using System.ComponentModel.DataAnnotations;

namespace Cars.ViewModels.AuthVm
{
    public class LoginVm
    {
        public string UserNameorEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember  { get; set; }
    }
}
