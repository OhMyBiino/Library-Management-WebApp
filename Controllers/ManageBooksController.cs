using LibraryManagement.Database;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class ManageBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageBooksController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task <IActionResult> ManageBooksPage()
        {
            var bookList = await _context.Books.ToListAsync();

            return View(bookList);
        }

        //CREATE
        public IActionResult CreateBook() 
        {

            return View();
        }

        //CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook([Bind("Title,Description,Author,Genre,ISBN,isBorrowed")] BookModel book) 
        {
            if (ModelState.IsValid) 
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageBooksPage));
            }
            return View(book);
        }

        //GET: DELETE
        [HttpGet]
        public async Task<IActionResult> DeleteBook(string? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == id);

            if (book == null) 
            {
                return NotFound();
            }
            return View(book);
        }

        //POST: DELETE
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookConfirmed(string id) 
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null) 
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageBooksPage));
        }

        private bool MovieExists(string id) 
        {
            return _context.Books.Any(b => b.ISBN == id);
        }
    }
}
