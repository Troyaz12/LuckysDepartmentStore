namespace LuckysDepartmentStore.Models
{
    public class Carts
    {
        public int ID { get; set; }
        public string CartID { get; set; }

        public int ProductID { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
