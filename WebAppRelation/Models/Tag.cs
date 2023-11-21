namespace WebAppRelation.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BooksTags> BooksTags { get; set; }
        public List<BlogsTags> BlogsTags { get; set; }
    }
}
