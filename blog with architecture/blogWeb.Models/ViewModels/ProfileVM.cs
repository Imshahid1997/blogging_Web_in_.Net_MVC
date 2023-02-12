using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace blogWeb.ViewModels
{
    public class ProfileVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Bio { get; set; }

        //? is use to accept the values even its null or empty
        public IFormFile? Image { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="password and confirm password doesn't match")]
        public string ConfirmPassword { get; set; }
        public string username { get; set; }
    }
}
