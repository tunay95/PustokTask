using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAppRelation.Controllers
{
    public class CartController : Controller
    {

        AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddBasket(int id)
        {
            if (id <= 0) return BadRequest();
            Book book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();

            BasketCookieItemVM basketCookieItem = new BasketCookieItemVM()
            {
                Id = id,
                Count = 1
            };
            var json = JsonConvert.SerializeObject(basketCookieItem);
            Response.Cookies.Append("Basket", json);



            return RedirectToAction("Home", "Home");
        }



        public IActionResult GetBasket()
        {
            var basketCookieJson = Request.Cookies["Basket"];
            return Content(basketCookieJson);
        }
    }
}
