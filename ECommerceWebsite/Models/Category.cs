using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public int category_name { get; set; }


    }
}
