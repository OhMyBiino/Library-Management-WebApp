using LibraryManagement.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookTransactionsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult BookTransactionsPage()
        {
            return View();
        }
    }
}
