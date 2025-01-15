namespace LuckysDepartmentStore.Models.DTO.Products
{
    public class ProductDetailDTO
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public Decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Brand { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public byte[]? ProductPicture { get; set; }
        public string? DiscountTags { get; set; }
        public string? SearchWords { get; set; }
    }
}
