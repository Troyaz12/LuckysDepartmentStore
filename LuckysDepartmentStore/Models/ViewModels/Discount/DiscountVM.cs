namespace LuckysDepartmentStore.Models.ViewModels.Discount
{
    public class DiscountVM
    {
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool DiscountActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? DiscountArt { get; set; }
    }
}
