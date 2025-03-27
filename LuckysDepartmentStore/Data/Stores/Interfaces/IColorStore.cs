using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IColorStore
    {
        public Task<List<ColorProductItemDTO>> ColorsByProductID(int productId);
        public Task<List<Color>> AllColors();
        public Task<List<Sizes>> AllSizes();
        public Task<int> AddColor(string name);
        public Task<string> GetColorName(int id);
        public Task<string> GetSizeName(int id);
        public Task<int> CreateSize(string name);
    }
}
