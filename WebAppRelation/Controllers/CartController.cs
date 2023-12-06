using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppRelation.ViewModel;
using WebAppRelation.ViewModels;

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
            var jsonCookie = Request.Cookies["Basket"];
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            if (jsonCookie != null)
            {
                var cookieItems = JsonConvert.DeserializeObject<List<CookieItemVM>>(jsonCookie);
                bool countCheck = false;

                List<CookieItemVM> deletedCookie = new List<CookieItemVM>();
                foreach (var item in cookieItems)
                {
                    Book book = _context.Books.Include(p => p.BookImages.Where(p => p.IsPrime == true)).FirstOrDefault(p => p.Id == item.Id);
                    if (book == null)
                    {
                        deletedCookie.Add(item);
                        continue;
                    }

                    basketItems.Add(new BasketItemVM()
                    {
                        Id = item.Id,
                        Title = book.Title,
                        Price = book.Price,
                        Count = item.Count

                    });
                }
                if (deletedCookie.Count > 0)
                {
                    foreach (var delete in deletedCookie)
                    {
                        cookieItems.Remove(delete);
                    }
                }

                    Response.Cookies.Append("Basket", JsonConvert.SerializeObject(cookieItems));
            }
            return View(basketItems);
        }

        public ActionResult AddBasket(int id)
        {
            if (id <= 0) return BadRequest();
            Book book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();

            CookieItemVM basketCookieItem = new CookieItemVM()
            {
                Id = id,
                Count = 1
            };
            List<CookieItemVM> basket;
            var json = Request.Cookies["Basket"];
            if(json != null)
            {
                basket = JsonConvert.DeserializeObject<List<CookieItemVM>>(json);   
                var existBook = basket.FirstOrDefault(b => b.Id == id);
                if (existBook != null)
                {
                    existBook.Count++;
                }
                else
                {
                    basket.Add(new CookieItemVM()
                    {
                        Id=id,
                        Count = 1
                    
                    });
                }

            }
            else
            {

                basket = new List<CookieItemVM>();
                basket.Add(new CookieItemVM()
                {
                    Id = id,
                    Count = 1
                });

            }





            var cookieBasket = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", cookieBasket);
            return RedirectToAction("Index");
        }

		public IActionResult RemoveBasketItem(int id)
		{
			var cookieBasket = Request.Cookies["Basket"];
			if (cookieBasket != null)
			{
				List<CookieItemVM> basket = JsonConvert.DeserializeObject<List<CookieItemVM>>(cookieBasket);

				var deleteElement = basket.FirstOrDefault(p => p.Id == id);
				if (deleteElement != null)
				{
					basket.Remove(deleteElement);
				}


				Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket));
				return Ok();
			}
			return NotFound();
		}


		public IActionResult GetBasket()
        {
            var basketCookieJson = Request.Cookies["Basket"];
            return Content(basketCookieJson);
        }
    }
}
