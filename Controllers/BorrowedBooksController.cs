using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BorrowedBooksController : Controller
    {
        public IActionResult BorrowedBooks()
        {
            return View();
        }
    }
}
