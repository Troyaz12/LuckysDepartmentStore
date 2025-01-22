using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IColorService
    {
        public int Create(string name);
        public string GetColorName(int id);
        public string GetSizeName(int id);
    }
}
