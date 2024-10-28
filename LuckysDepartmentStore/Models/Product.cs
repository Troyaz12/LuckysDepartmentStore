namespace Lucky_sDepartmentStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public Decimal Price { get; set; }  
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string Brand { get; set; }
        public int ColorProductID { get; set; }
        public int SubCategoryID { get; set; }
        public byte[]? ProductPicture { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
