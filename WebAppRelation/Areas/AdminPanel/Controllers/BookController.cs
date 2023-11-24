using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppRelation.Areas.AdminPanel.ViewModels;

namespace WebAppRelation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BookController : Controller
    {
        AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> TableAsync()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.books = _context.Books.Include(b => b.Category).Include(b => b.Brand).ToList();
            return View(adminVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            book.Availability = false;
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Table");
        }
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Table");
        }
        public IActionResult Update(int id)
        {
            Book book = _context.Books.Find(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book newBook)
        {
            Book oldBook = _context.Books.Find(newBook.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            oldBook.Title = newBook.Title;
            oldBook.Description = newBook.Description;
            oldBook.Author = newBook.Author;
            oldBook.Price = newBook.Price;
            oldBook.BookCode = newBook.BookCode;

            _context.SaveChanges();
            return RedirectToAction("Table");
        }
    }
}
