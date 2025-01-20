namespace LuckysDepartmentStore.Models.ViewModels.ShoppingCart
{
    public class CartsVM
    {
        public int ID { get; set; }
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? ProductPicture { get; set; }
        public string? ProductImage { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal SalePrice { get; set; }
    }
}
