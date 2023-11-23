using Microsoft.AspNetCore.Mvc;
using WebAppRelation.Areas.AdminPanel.ViewModels;

namespace WebAppRelation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BookController : Controller
    {
        AppDbContext _db;
        public BookController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Table()
        {
            AdminVM admin = new AdminVM();
            admin.Books = _db.Books
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .ToList();
            return View(admin);
        }
    }
}
