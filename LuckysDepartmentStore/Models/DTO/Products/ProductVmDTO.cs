using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.DTO.Products
{
    public class ProductVmDTO
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
        public string? Keywords { get; set; }
    }
}
