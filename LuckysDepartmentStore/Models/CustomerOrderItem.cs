namespace Lucky_sDepartmentStore.Models
{
    public class CustomerOrderItem
    {
        public int CustomerOrderItemID { get; set; }
        public int ProductID { get; set; }
        public int CustomerOrderID { get; set; }
        public int PaymentID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
