using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Models
{
    public class Feedback
    {
        [Key]
        public int feedback_id { get; set; }
        public string feedback_name { get; set; }
        public string feedback_message { get; set; }
    }
}
