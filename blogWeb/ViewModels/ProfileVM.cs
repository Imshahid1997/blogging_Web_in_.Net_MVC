namespace blogWeb.ViewModels
{
    public class ProfileVM
    {
    
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Bio { get; set; }
        public IFormFile Image { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string username { get; set; }
    }
}
