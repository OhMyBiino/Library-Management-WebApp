using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AvailableBooksController : Controller
    {
        public IActionResult AvailableBooks()
        {
            return View();
        }
    }
}
