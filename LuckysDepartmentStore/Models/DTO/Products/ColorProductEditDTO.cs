namespace LuckysDepartmentStore.Models.DTO.Products
{
    public class ColorProductEditDTO
    {
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int ColorProductID { get; set; }
        public int SizeID { get; set; }
    }
}
