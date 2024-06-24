namespace CLDV6211POE.Models
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCart> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? TotalQuantity {get; set;}
    }
}
