namespace WebTraSua.Model
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
    }
}
