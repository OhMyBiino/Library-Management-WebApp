using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult ListOfUsersPage()
        {
            return View();
        }
    }
}
