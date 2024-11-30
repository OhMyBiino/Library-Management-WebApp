using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
