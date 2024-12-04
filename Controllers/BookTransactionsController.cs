using LibraryManagement.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> BookTransactionsPage() 
        {
            var transactions = await _context.Transactions.ToListAsync();

            return View(transactions);
        }
    }
}
