using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Models
{
    public class Faqs
    {
        [Key]
        public int faqs_id { get; set; }
        public string faqs_name { get; set; }
        public string Faqs_message { get; set; }
    }
}
