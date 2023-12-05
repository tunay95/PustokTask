using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAppRelation.Areas.AdminPanel.ViewModels;
using WebAppRelation.Helpers;
using WebAppRelation.Models;

namespace WebAppRelation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BookController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _env; 
        public BookController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> TableAsync()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.books = _context.Books.Include(b => b.Category).Include(b => b.Brand).ToList();
            return View(adminVM);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductVM createProductVM)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool resultCategory=_context.Categories.Any(c => c.Id == createProductVM.CategoryId);
            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "Fidan eledi hamisini");
                return View();
            }

            Book book = new Book()
            {
                Title = createProductVM.Title,
                Description = createProductVM.Description,
                Price = createProductVM.Price,
                Author = createProductVM.Author,
                BookCode = createProductVM.BookCode,
                CategoryId = createProductVM.CategoryId  
                 

            };

            if (createProductVM.MainPhoto.CheckType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "Duzgun formatda sekil daxil et");
                return View();  
            }

            if (!createProductVM.MainPhoto.CheckLength(3000))
            {
                ModelState.AddModelError("MainPhoto", "max photo size - 3mb");
                return View();
            }


            if (createProductVM.HoverPhoto.CheckType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "Duzgun formatda sekil daxil et");
                return View();
            }

            if (!createProductVM.HoverPhoto.CheckLength(3000))
            {
                ModelState.AddModelError("MainPhoto", "max photo size - 3mb");
                return View();
            }

            BookImages mainImages = new BookImages()
            {
                IsPrime = true,
                ImgUrl = createProductVM.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product"),
                Book = book
            };

            BookImages hoverImages = new BookImages()
            {
                IsPrime = false,
                ImgUrl = createProductVM.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product"),
                Book = book
            };

            book.BookImages.Add(mainImages);
            book.BookImages.Add(hoverImages);

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Table");
        }
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if(book is null)
            {

                return View("Error");
            }
            
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
        public async Task<IActionResult> Update(int id)
        {

            Book book = await _context.Books.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (book is null)
            {

                return View("Error");
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            UpdateProductVM updateProductVM = new UpdateProductVM()
            {
                Id = id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Author = book.Author,
                BookCode = book.BookCode,
                CategoryId = book.CategoryId


            };

            return View(updateProductVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM updateProductVM)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            Book oldBook = await _context.Books.Where(b=>b.Id==updateProductVM.Id).FirstOrDefaultAsync();

            if (oldBook == null)
            {

                return View("Error");
            }

            bool resultCategory=await _context.Categories.AnyAsync(b=>b.Id == updateProductVM.CategoryId);

            if (!resultCategory)
            {

                ModelState.AddModelError("CategoryId", "No such category exists");
                return View();
            }

            oldBook.Title = updateProductVM.Title;
            oldBook.Description = updateProductVM.Description;
            oldBook.Author = updateProductVM.Author;
            oldBook.Price = updateProductVM.Price;
            oldBook.BookCode = updateProductVM.BookCode;
            oldBook.CategoryId = updateProductVM.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction("Table");
        }
    }
}
