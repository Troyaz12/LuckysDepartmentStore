namespace LuckysDepartmentStore.Models.ViewModels.Checkout
{
    public class OrderModelVM
    {
        public List<ShippingAddress> Shipping { get; set; }
        public List<Payment> Payment { get; set; }
    }
}
