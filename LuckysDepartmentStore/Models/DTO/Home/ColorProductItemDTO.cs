namespace LuckysDepartmentStore.Models.DTO.Home
{
    public class ColorProductItemDTO
    {
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int ColorProductID { get; set; }
        public int SizeID { get; set; }
        public string SizeName {  get; set; }
    }
}
