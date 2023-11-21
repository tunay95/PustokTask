

namespace WebAppRelation.DAL;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogsTags> BlogsTags { get; set; }
    public DbSet<BookImages> BookImages { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BooksTags> BooksTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
}
