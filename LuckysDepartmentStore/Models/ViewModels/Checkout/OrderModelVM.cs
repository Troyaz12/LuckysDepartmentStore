using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace LuckysDepartmentStore.Models.ViewModels.Checkout
{
    public class OrderModelVM
    {
        public List<ShippingAddressVM> Shipping { get; set; }
        public List<PaymentOptionsVM> Payment { get; set; }
    }
}
