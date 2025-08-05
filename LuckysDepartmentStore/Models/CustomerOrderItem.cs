using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class CustomerOrderItem
    {
        public int CustomerOrderItemID { get; set; }
        public int ProductID { get; set; }
        public int CustomerOrderID { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ShippingID { get; set; }
    }
}
