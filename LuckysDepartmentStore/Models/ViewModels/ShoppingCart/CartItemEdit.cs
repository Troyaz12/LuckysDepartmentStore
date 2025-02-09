namespace LuckysDepartmentStore.Models.ViewModels.ShoppingCart
{
    public class CartItemEdit
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int ColorSelection { get; set; }
        public int SizeSelection { get; set; }
        public decimal Price { get; set; }
    }
}
