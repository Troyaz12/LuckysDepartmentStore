using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public interface IBrandService
    {
       public int? Create(ProductVM product);
    }
}
