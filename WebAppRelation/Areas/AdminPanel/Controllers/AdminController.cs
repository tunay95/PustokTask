using Microsoft.AspNetCore.Mvc;
using WebAppRelation.Areas.AdminPanel.ViewModels;

namespace WebAppRelation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        AppDbContext _db;
        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
