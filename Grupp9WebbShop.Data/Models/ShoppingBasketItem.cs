namespace Grupp9WebbShop.Data.Models
{
    public class ShoppingBasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // one item
        public decimal RowPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}