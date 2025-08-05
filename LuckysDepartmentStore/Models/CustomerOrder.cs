using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class CustomerOrder
    {
        public int CustomerOrderID { get; set; }
        public int ShippingID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PaymentID { get; set; }
        public virtual Shipping CustomerShippingData { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
