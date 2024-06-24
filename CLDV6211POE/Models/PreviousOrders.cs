using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE.Models
{
    public class PreviousOrders
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime? OrderPurchaseDate { get; set; }
        public decimal? Total {  get; set; }
        public string Status { get; set; }
    }
}
