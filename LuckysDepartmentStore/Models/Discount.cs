namespace LuckysDepartmentStore.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool DiscountActive {  get; set; }        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? DiscountArt { get; set; }
        public string? DiscountDescription { get; set; }
        public int? SubCategoryID { get; set; }
        public int? CategoryID { get; set; }
        public int? ProductID { get; set; }
        public int? BrandID { get; set; }
        public string? Keywords { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
