using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult ListOfBooksPage()
        {
            return View();
        }
    }
}
