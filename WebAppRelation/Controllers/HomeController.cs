using WebAppRelation.DAL;

namespace WebAppRelation.Controllers;

public class HomeController : Controller
{
    AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
}