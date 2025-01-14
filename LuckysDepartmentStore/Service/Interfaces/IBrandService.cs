using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IBrandService
    {
        public int Create(ProductCreateVM product);
    }
}
