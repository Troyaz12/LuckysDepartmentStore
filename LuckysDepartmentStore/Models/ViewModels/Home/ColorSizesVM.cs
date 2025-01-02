namespace LuckysDepartmentStore.Models.ViewModels.Home
{
    public class ColorSizesVM
    {
        public int? ColorID { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int ColorProductID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public string SelectedColor { get; set; }
    }
}
