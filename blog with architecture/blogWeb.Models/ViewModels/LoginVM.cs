using System.ComponentModel.DataAnnotations;

namespace blogWeb.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "user name is required")]
        public string username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="password is required")]
        public string password { get; set; }
    }
}
