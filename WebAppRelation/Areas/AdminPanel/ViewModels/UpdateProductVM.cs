namespace WebAppRelation.Areas.AdminPanel.ViewModels
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }

    }
}
