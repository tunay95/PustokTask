using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppRelation.Models;
using WebAppRelation.ViewModel;
using WebAppRelation.ViewModels;

namespace WebAppRelation.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
       

        public CartController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
           
        }

        public async Task<IActionResult> Index()
        {
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BasketItem> userBaskets = await _context.BasketItems
                    .Where(b => b.AppUserId == user.Id)
                    .Include(b => b.Book)
                    .ThenInclude(p => p.BookImages.Where(pi => pi.IsPrime == true))
                    .Where(b => !b.Book.IsDeleted).ToListAsync();
                foreach (var item in userBaskets)
                {
                    basketItems.Add(new BasketItemVM()
                    {
                        Title = item.Book.Title,
                        Price = item.Price,
                        Count = item.Count,
                    });
                }
            }
            else
            {


                var jsonCookie = Request.Cookies["Basket"];

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
                            Count = item.Count,
                            //ImgUrl = book.BookImages.FirstOrDefault().ImgUrl
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

            }


            return View(basketItems);
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            if (id <= 0) return BadRequest();
            Book book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem oldItem = _context.BasketItems.FirstOrDefault(b => b.AppUserId == appUser.Id && b.BookId == id);

                if (oldItem == null)
                {
                    BasketItem newItem = new BasketItem()
                    {
                        AppUser = appUser,
                        Book = book,
                        Price = book.Price,
                        Count = 1
                    };
                    _context.BasketItems.Add(newItem);
                }
                else
                {
                    oldItem.Count += 1;
                }
                await _context.SaveChangesAsync();

            }
            else
            {
               
                List<CookieItemVM> basket;
                var json = Request.Cookies["Basket"];
                if (json != null)
                {
                    basket = JsonConvert.DeserializeObject<List<CookieItemVM>>(json);
                    var existBook = basket.FirstOrDefault(b => b.Id == id);
                    if (existBook != null)
                    {
                        existBook.Count+=1;
                    }
                    else
                    {
                        basket.Add(new CookieItemVM()
                        {
                            Id = id,
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


            }
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
