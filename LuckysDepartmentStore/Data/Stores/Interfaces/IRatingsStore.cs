using LuckysDepartmentStore.Models.DTO.Home;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IRatingsStore
    {
        Task<List<RatingsDTO>> RatingsByProductID(int productId);


    }
}
