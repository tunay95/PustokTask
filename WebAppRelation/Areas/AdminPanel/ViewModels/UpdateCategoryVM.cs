namespace WebAppRelation.Areas.AdminPanel.ViewModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int>? ParentCategoryId { get; set; }
        public ICollection<Category>? categories { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
