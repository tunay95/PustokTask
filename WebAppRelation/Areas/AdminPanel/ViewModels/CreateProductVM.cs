namespace WebAppRelation.Areas.AdminPanel.ViewModels
{
    public class CreateProductVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public int Price { get; set; }
        public int? CategoryId { get; set; }
        
    }
}
