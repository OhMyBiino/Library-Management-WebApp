using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookTransactionsController : Controller
    {
        public IActionResult BookTransactions()
        {
            return View();
        }
    }
}
