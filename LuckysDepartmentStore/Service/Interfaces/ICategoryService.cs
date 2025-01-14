using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface ICategoryService
    {
        public int Create(ProductCreateVM product);
    }
}
