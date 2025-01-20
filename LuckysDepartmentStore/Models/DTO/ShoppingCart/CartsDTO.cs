namespace LuckysDepartmentStore.Models.DTO.ShoppingCart
{
    public class CartsDTO
    {
        public int ID { get; set; }
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? ProductPicture { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string? DiscountTag { get; set; }
    }
}
