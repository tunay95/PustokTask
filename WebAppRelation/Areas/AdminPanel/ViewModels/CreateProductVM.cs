using System.ComponentModel.DataAnnotations;

namespace WebAppRelation.Areas.AdminPanel.ViewModels
{
    public class CreateProductVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        [Required]
        public IFormFile MainPhoto { get; set; }
        [Required]
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile> Photos { get; set; }

    }
}
