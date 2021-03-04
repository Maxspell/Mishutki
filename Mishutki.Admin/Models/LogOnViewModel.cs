using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mishutki.Admin.Models
{
    public class LogOnViewModel
    {
        [DisplayName("Login:")]
        [Required(ErrorMessage = "Введите логин")]
        public string UserName { get; set; }

        [DisplayName("Password:")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}