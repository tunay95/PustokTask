
using WebAppRelation.Areas.AdminPanel.ViewModels;

namespace WebAppRelation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.categories = _context.Categories.ToList();
            return View(adminVM);
        }
        public IActionResult Create()
        {


            ICollection<Category> categories = _context.Categories.ToList();
            CreateCategoryVM categoryVM = new CreateCategoryVM()
            {
                categories = categories
            };
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryVM categoryVM)
        {

            Category category = new Category();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (categoryVM.ParentCategoryId != "null")
            {
                category.ParentCategoryId = int.Parse(categoryVM.ParentCategoryId);
            }
            category.Name = categoryVM.Name;

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Categories =  _context.Categories.ToList();

            Category category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            ViewBag.Categories =  _context.Categories.ToList();

            Category oldCategory = _context.Categories.Find(newCategory.Id);

            if(!ModelState.IsValid)
            {
                return View();
            }
            oldCategory.Name = newCategory.Name;
            oldCategory.ParentCategoryId = newCategory.ParentCategoryId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
