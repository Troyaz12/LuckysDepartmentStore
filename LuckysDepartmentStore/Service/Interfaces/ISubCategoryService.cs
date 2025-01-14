using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface ISubCategoryService
    {
        public int Create(ProductCreateVM product);
    }
}
