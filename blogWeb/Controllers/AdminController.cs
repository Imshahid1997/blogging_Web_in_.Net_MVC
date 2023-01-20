using Microsoft.AspNetCore.Mvc;

namespace blogWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
