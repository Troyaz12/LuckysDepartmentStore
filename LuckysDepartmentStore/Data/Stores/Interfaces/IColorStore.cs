using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IColorStore
    {
        public Task<List<ColorProductItemDTO>> ColorsByProductID(int productId);

        public Task<List<Color>> AllColors();

        public Task<List<Sizes>> AllSizes();

    }
}
