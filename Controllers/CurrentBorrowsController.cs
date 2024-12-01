using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class CurrentBorrowsController : Controller
    {
        public IActionResult ListOfBorrowedBooksPage()
        {
            return View();
        }
    }
}
