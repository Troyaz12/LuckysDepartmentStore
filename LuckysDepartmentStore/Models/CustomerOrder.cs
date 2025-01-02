namespace LuckysDepartmentStore.Models
{
    public class CustomerOrder
    {
        public int CustomerOrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int PaymentID { get; set; }
        public string UserName { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
