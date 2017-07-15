namespace OnlineShop.WebAPI.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { set; get; }

        public int ProductID { set; get; }

        public decimal Price { set; get; }
        public int Quantity { set; get; }
    }
}