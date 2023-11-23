namespace WebAppRelation.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Book { get; set; }
        public List<Blog> Blog { get; set; }
    }
}
