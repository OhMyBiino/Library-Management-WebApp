using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> ListOfBooksPage()
        {
            var bookCollection = await _context.Books.ToListAsync();

            return View(bookCollection);
        }

        [HttpGet]
        public async Task<IActionResult> BookDetails(string? id) 
        {
            var book = await _context.Books.FindAsync(id);

            return View(book);
        }
    }
}
