using System.Data;

namespace WebAppRelation.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public DataSetDateTime CreateDate { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
       


    }
}
