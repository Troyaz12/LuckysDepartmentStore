namespace Lucky_sDepartmentStore.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public Decimal DiscountPercent { get; set; }
        public Decimal DiscountAmount { get; set; }
        public bool DiscountActive {  get; set; }
        public int ProductID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? DiscountArt { get; set; }
    }
}
