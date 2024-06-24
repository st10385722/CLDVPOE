using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderPurchaseDate { get; set; }
        public decimal? Total { get; set; }
        public string Status { get; set; }
    }
}
