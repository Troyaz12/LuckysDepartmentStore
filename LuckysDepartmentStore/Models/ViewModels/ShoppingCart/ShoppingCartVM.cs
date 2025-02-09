namespace LuckysDepartmentStore.Models.ViewModels.ShoppingCart
{
    public class ShoppingCartVM
    {
        public List<CartsVM> cartsVMs { get; set; } = new List<CartsVM>();
        public Decimal CartTotal { get; set; }
        public int CartSum { get; set; }
    }
}
