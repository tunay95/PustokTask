namespace WebAppRelation.ViewComponents
{
    public class BookViewComponent:ViewComponent
    {
        AppDbContext _context;

        public BookViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Product=_context.Books
                .Include(x=>x.BookImages)
                .ToList();

            return View(Product);
        }
    }
}
