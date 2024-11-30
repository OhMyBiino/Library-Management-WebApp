using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ManageBooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
