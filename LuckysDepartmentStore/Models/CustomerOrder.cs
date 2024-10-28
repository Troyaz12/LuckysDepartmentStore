namespace Lucky_sDepartmentStore.Models
{
    public class CustomerOrder
    {
        public int CustomerOrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
