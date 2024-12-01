using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ManageBooksController : Controller
    {
        public IActionResult ManageBooksPage()
        {
            return View();
        }
    }
}
