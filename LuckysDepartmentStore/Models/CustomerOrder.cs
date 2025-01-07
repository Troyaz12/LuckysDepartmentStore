namespace LuckysDepartmentStore.Models
{
    public class CustomerOrder
    {
        public int CustomerOrderID { get; set; }
        public int ShippingAddressID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int PaymentID { get; set; }
        public virtual ShippingAddress Customer { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
