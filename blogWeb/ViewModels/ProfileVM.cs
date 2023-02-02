using System.ComponentModel.DataAnnotations;

namespace blogWeb.ViewModels
{
    public class ProfileVM
    {
    
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Bio { get; set; }
        public IFormFile Image { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="password and confirm password doesn't match")]
        public string ConfirmPassword { get; set; }
        public string username { get; set; }
    }
}
