﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Areas.Identity.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Member")]
    public class CurrentBorrowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LibraryManagementUser> _userManager;
        public CurrentBorrowsController(ApplicationDbContext context,
                UserManager<LibraryManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListOfBorrowedBooksPage()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserID = currentUser.Id;

            var BorrowedBooksStringISBN = await _context.Transactions
                .Where(transaction => transaction.UserID == currentUserID && transaction.Status == "Borrowed")
                //ADD && transaction.isReturned (kelangan mamodify to true ang table if naisoli na) pano mamomodify?
                //fetch ulit ang lahat ng transactions at hanapin ang Transaction gamit ang bookISBN, icheck kung yung current ID ng user e match sa Transaction.UserID. Then modify mo na into True
                .Select(transactionISBN => transactionISBN.BookISBN)
                .ToListAsync();

            var myBorrowedBooks = await _context.Books
                .Where(book => BorrowedBooksStringISBN.Contains(book.ISBN))
                .ToListAsync();

            return View(myBorrowedBooks);
        }

        //NOT IMPLEMENTED SINCE KELANGAN NG TRANSACTION DUEDATE, which means new adjustment sa Model
        //public async Task<IActionResult> BookDetails(string? id) 
        //{  
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //        return NotFound();


        //    return View(book);
        //}

        //SEARCHING ALGORITHM ISSUES
        //[HttpGet]
        //public async Task<IActionResult> SearchBooks(string? query)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    var currentUserID = currentUser.Id;

        //    var BorrowedBooksStringISBN = await _context.Transactions
        //        .Where(transaction => transaction.UserID == currentUserID && transaction.Status == "Borrowed")
        //        .Select(transactionISBN => transactionISBN.BookISBN)
        //        .ToListAsync();

        //    var myBorrowedBooks = await _context.Books
        //        .Where(book => BorrowedBooksStringISBN.Contains(book.ISBN))
        //        .ToListAsync();

        //    var bookSearchedCollection = myBorrowedBooks.Where(b => b.Title.Contains(query) ||
        //            b.Author.Contains(query) || b.Genre.Contains(query) || b.ISBN.Contains(query))
        //        .ToList() ;

        //    return View("ListOfBorrowedBooksPage", bookSearchedCollection);
        //}

        [HttpGet]
        public async Task<IActionResult> ReturnBook(string? id) 
        {
            if (id == null)
            {
                return NotFound("ISBN is Null");
            }
            var bookToReturn = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == id);

            if (bookToReturn == null) {
                return NotFound("Book Not Found");
            }

            return View(bookToReturn);
        }

        [HttpPost, ActionName("ReturnBook")]
        public async Task<IActionResult> ReturnBookConfirmed(string? id)
        {

            if (id == null) 
            {
                return NotFound("ISBN NULL");
            }
            
            var bookToReturn = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == id);

            if (bookToReturn != null) 
            {
                bookToReturn.isBorrowed = false;
                _context.Update(bookToReturn);

                //Get current user
                var currentUser = await _userManager.GetUserAsync(User);

                //Modify Transaction History
                var prevTransaction = await _context.Transactions
                        .FirstOrDefaultAsync(transaction =>
                            transaction.BookISBN == id &&
                            transaction.UserID == currentUser.Id &&
                            transaction.Status == "Borrowed");


                //Update transactions object after finding the book
                if (prevTransaction != null) 
                {
                    prevTransaction.Status = "Returned";
                    _context.Update(prevTransaction);
                }
                

                //Make transaction info
                var newTransaction = new TransactionModel()
                {
                    UserID = currentUser.Id,
                    BorrowerName = $"{currentUser.FirstName} {currentUser.LastName}",
                    TransactionType = "Return",
                    BookISBN = bookToReturn.ISBN,
                    Status = "Returned"
                };

                //add newTransaction to Transactions
                _context.Transactions.Add(newTransaction);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListOfBorrowedBooksPage));
            }

            return NotFound("Book Not Found");
        }
    }
}
