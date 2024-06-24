using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name ="Name")]
        public string? ProductName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string? ProductCategory { get; set;}
        [Required]
        [Display(Name = "Availability")]
        public int ProductAvailability { get; set; }

        public string? ImageFileName { get; set; }
    }
}
