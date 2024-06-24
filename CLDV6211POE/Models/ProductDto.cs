using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; } // New property for the product ID

        [Required]
        [Display(Name = "Name")]
        public string? ProductName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string? ProductCategory { get; set; }
        [Required]
        [Display(Name = "Availability")]
        public int ProductAvailability { get; set; }
        [Display(Name = "Image")]
        public IFormFile? ProductImageFile { get; set; }
    }
}
