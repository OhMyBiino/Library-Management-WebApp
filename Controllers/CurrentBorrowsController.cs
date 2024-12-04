using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Member")]
    public class CurrentBorrowsController : Controller
    {
        public IActionResult ListOfBorrowedBooksPage()
        {
            return View();
        }
    }
}
