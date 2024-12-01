using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AvailableBooksController : Controller
    {
        public IActionResult ListOfAvailableBooksPage()
        {
            return View();
        }
    }
}
