using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public interface IColorService
    {
        public int? Create(ProductVM product);
        public int? AddProductColor(List<ColorProductVM> product);
    }
}
