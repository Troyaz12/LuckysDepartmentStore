using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckysDepartmentStore.Models.ViewModels.Product
{
    public class ProductListVM
    {
        public List<ProductVM> Products { get; set; }
        public SelectList? Categories { get; set; }
        public string? Category { get; set; }
        public string? SearchString { get; set; }
    }
}
