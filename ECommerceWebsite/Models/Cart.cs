using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Models
{
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }
        public int prod_id { get; set; }
        public int cust_id { get; set; }
        public int cart_quantity { get; set; }
        public int cart_status { get; set; }
      
    }
}
